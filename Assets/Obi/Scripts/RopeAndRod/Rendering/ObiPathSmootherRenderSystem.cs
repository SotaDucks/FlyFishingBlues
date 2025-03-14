﻿using System.Runtime.InteropServices;
using UnityEngine;
using Unity.Profiling;

namespace Obi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BurstPathSmootherData
    {
        public uint smoothing;
        public float decimation;
        public float twist;
        public float restLength;
        public float smoothLength;
        public uint usesOrientedParticles;

        public BurstPathSmootherData(ObiRopeBase rope, ObiPathSmoother smoother)
        {
            smoothing = smoother.smoothing;
            decimation = smoother.decimation;
            twist = smoother.twist;
            usesOrientedParticles = (uint)(rope.usesOrientedParticles ? 1 : 0);
            restLength = rope.restLength;
            smoothLength = 0;
        }
    }

    public abstract class ObiPathSmootherRenderSystem : RenderSystem<ObiPathSmoother>
    {
        public Oni.RenderingSystemType typeEnum { get => Oni.RenderingSystemType.AllSmoothedRopes; }

        public RendererSet<ObiPathSmoother> renderers { get; } = new RendererSet<ObiPathSmoother>();

        static protected ProfilerMarker m_SetupRenderMarker = new ProfilerMarker("SetupSmoothPathRendering");
        static protected ProfilerMarker m_RenderMarker = new ProfilerMarker("SmoothPathRendering");

        protected ObiSolver m_Solver;

        public ObiNativeList<int> particleIndices;
        public ObiNativeList<int> chunkOffsets; /**< for each actor, index of the first chunk */

        public ObiNativeList<BurstPathSmootherData> pathData; /**< for each chunk, smoother params/data.*/

        public ObiNativeList<ObiPathFrame> rawFrames;
        public ObiNativeList<int> rawFrameOffsets;      /**< index of the first frame for each chunk.*/
        public ObiNativeList<int> decimatedFrameCounts; /**< amount of frames in each chunk, after decimation.*/

        public ObiNativeList<ObiPathFrame> smoothFrames;
        public ObiNativeList<int> smoothFrameOffsets;  /**< index of the first frame for each chunk.*/
        public ObiNativeList<int> smoothFrameCounts;  /**< amount of smooth frames for each chunk.*/

        // path smoothing must be done before all other rope render systems, which are on a higher tier.
        public uint tier
        {
            get { return 0; }
        }

        public ObiPathSmootherRenderSystem(ObiSolver solver)
        {
            m_Solver = solver;

            pathData = new ObiNativeList<BurstPathSmootherData>();
            particleIndices = new ObiNativeList<int>();
            chunkOffsets = new ObiNativeList<int>();

            rawFrames = new ObiNativeList<ObiPathFrame>();
            rawFrameOffsets = new ObiNativeList<int>();

            decimatedFrameCounts = new ObiNativeList<int>();

            smoothFrames = new ObiNativeList<ObiPathFrame>();
            smoothFrameOffsets = new ObiNativeList<int>();
            smoothFrameCounts = new ObiNativeList<int>();
        }

        public void Dispose()
        {
            if (particleIndices != null)
                particleIndices.Dispose();
            if (chunkOffsets != null)
                chunkOffsets.Dispose();

            if (pathData != null)
                pathData.Dispose();

            if (rawFrames != null)
                rawFrames.Dispose();
            if (rawFrameOffsets != null)
                rawFrameOffsets.Dispose();
            if (decimatedFrameCounts != null)
                decimatedFrameCounts.Dispose();

            if (smoothFrames != null)
                smoothFrames.Dispose();
            if (smoothFrameOffsets != null)
                smoothFrameOffsets.Dispose();
            if (smoothFrameCounts != null)
                smoothFrameCounts.Dispose();
        }

        private void Clear()
        {
            pathData.Clear();
            particleIndices.Clear();
            chunkOffsets.Clear();

            rawFrames.Clear();
            rawFrameOffsets.Clear();
            decimatedFrameCounts.Clear();

            smoothFrames.Clear();
            smoothFrameOffsets.Clear();
            smoothFrameCounts.Clear();
        }

        private int GetChaikinCount(int initialPoints, uint recursionLevel)
        {
            if (recursionLevel <= 0 || initialPoints < 3)
                return initialPoints;

            // calculate amount of new points generated by each inner control point:
            int pCount = (int)Mathf.Pow(2, recursionLevel);
            return (initialPoints - 2) * pCount + 2;
        }

        public virtual void  Setup()
        {
            using (m_SetupRenderMarker.Auto())
            {
                Clear();

                int actorCount = 0;
                int chunkCount = 0;
                int rawFrameCount = 0;

                for (int i = 0; i < renderers.Count; ++i)
                {
                    var renderer = renderers[i];

                    var rope = renderer.actor as ObiRopeBase;
                    var data = new BurstPathSmootherData(rope, renderer);

                    chunkOffsets.Add(chunkCount);

                    // iterate trough elements, finding discontinuities as we go:
                    for (int e = 0; e < rope.elements.Count; ++e)
                    {
                        rawFrameCount++;
                        particleIndices.Add(rope.elements[e].particle1);

                        // At discontinuities, start a new chunk.
                        if (e < rope.elements.Count - 1 && rope.elements[e].particle2 != rope.elements[e + 1].particle1)
                        {
                            rawFrameOffsets.Add(++rawFrameCount);
                            particleIndices.Add(rope.elements[e].particle2);
                            pathData.Add(data);
                            chunkCount++;
                        }
                    }

                    chunkCount++;
                    rawFrameOffsets.Add(++rawFrameCount);
                    particleIndices.Add(rope.elements[rope.elements.Count - 1].particle2);
                    pathData.Add(data);

                    // store the index in this system, so that other render systems
                    // in higher tiers can easily access smooth path data:
                    renderer.indexInSystem = actorCount++;
                }

                // Add last entry (total amount of chunks):
                chunkOffsets.Add(chunkCount);

                // resize storage:
                rawFrames.ResizeUninitialized(rawFrameCount);
                decimatedFrameCounts.ResizeUninitialized(rawFrameOffsets.count);
                smoothFrameOffsets.ResizeUninitialized(rawFrameOffsets.count);
                smoothFrameCounts.ResizeUninitialized(rawFrameOffsets.count);

                // calculate smooth chunk counts:
                int smoothFrameCount = 0;
                for (int i = 0; i < rawFrameOffsets.count; ++i)
                {
                    int frameCount = rawFrameOffsets[i] - (i > 0 ? rawFrameOffsets[i - 1] : 0);

                    int smoothCount = GetChaikinCount(frameCount, pathData[i].smoothing);

                    smoothFrameOffsets[i] = smoothFrameCount;
                    smoothFrameCounts[i] = smoothCount;
                    smoothFrameCount += smoothCount;
                }

                smoothFrames.ResizeUninitialized(smoothFrameCount);
            }
        }

        public int GetChunkCount(int rendererIndex)
        {
            rendererIndex = Mathf.Clamp(rendererIndex, 0, renderers.Count);

            if (rendererIndex >= chunkOffsets.count)
                return 0;

            return chunkOffsets[rendererIndex + 1] - chunkOffsets[rendererIndex];
        }


        public int GetSmoothFrameCount(int rendererIndex)
        {
            rendererIndex = Mathf.Clamp(rendererIndex, 0, renderers.Count);

            int frameCount = 0;

            if (rendererIndex >= chunkOffsets.count)
                return frameCount;

            for (int i = chunkOffsets[rendererIndex]; i < chunkOffsets[rendererIndex + 1]; ++i)
                frameCount += smoothFrameCounts[i];

            return frameCount;
        }

        public int GetSmoothFrameCount(int rendererIndex, int chunkIndex)
        {
            rendererIndex = Mathf.Clamp(rendererIndex, 0, renderers.Count);

            if (rendererIndex >= chunkOffsets.count)
                return 0;

            int chunkCount = chunkOffsets[rendererIndex + 1] - chunkOffsets[rendererIndex];
            int chunk = chunkOffsets[rendererIndex] + Mathf.Clamp(chunkIndex, 0, chunkCount);

            return smoothFrameCounts[chunk];
        }

        public float GetSmoothLength(int rendererIndex)
        {
            rendererIndex = Mathf.Clamp(rendererIndex, 0, renderers.Count);

            float smoothLength = 0;

            if (rendererIndex >= chunkOffsets.count)
                return smoothLength;

            for (int i = chunkOffsets[rendererIndex]; i < chunkOffsets[rendererIndex + 1]; ++i)
                smoothLength += pathData[i].smoothLength;

            return smoothLength;
        }

        public ObiPathFrame GetFrameAt(int rendererIndex, int chunkIndex, int frameIndex)
        {
            rendererIndex = Mathf.Clamp(rendererIndex, 0, renderers.Count);

            if (rendererIndex >= chunkOffsets.count)
                return ObiPathFrame.Identity;

            int chunkCount = chunkOffsets[rendererIndex + 1] - chunkOffsets[rendererIndex];
            int chunk = chunkOffsets[rendererIndex] + Mathf.Clamp(chunkIndex, 0, chunkCount);

            return smoothFrames[smoothFrameOffsets[chunk] + Mathf.Clamp(frameIndex, 0, smoothFrameCounts[chunk])];
        }

        public ObiPathFrame GetFrameAt(int rendererIndex, float mu)
        {
            rendererIndex = Mathf.Clamp(rendererIndex, 0, renderers.Count);

            if (rendererIndex >= chunkOffsets.count)
                return ObiPathFrame.Identity;

            float length = 0;
            for (int i = chunkOffsets[rendererIndex]; i < chunkOffsets[rendererIndex + 1]; ++i)
                length += pathData[i].smoothLength;

            length *= mu;

            // iterate trough all chunks:
            float lerp = 0;
            int frame = 0;
            for (int i = chunkOffsets[rendererIndex]; i < chunkOffsets[rendererIndex + 1]; ++i)
            {
                int firstFrame = smoothFrameOffsets[i];
                int frameCount = smoothFrameCounts[i];

                // iterate trough all frames in this chunk, accumulating distance:
                for (int j = firstFrame + 1; j < firstFrame + frameCount; ++j)
                {
                    float frameDistance = Vector3.Distance(smoothFrames[j - 1].position,
                                                           smoothFrames[j].position);

                    lerp = length / frameDistance;
                    length -= frameDistance;
                    frame = j;

                    if (length <= 0)
                        return (1 - lerp) * smoothFrames[j - 1] + lerp * smoothFrames[j];
                }
            }

            // if no chunks/no frames, return default frame.
            return (1 - lerp) * smoothFrames[frame - 1] + lerp * smoothFrames[frame];
        }

        public void Step()
        {
        }

        public virtual void Render()
        {
            // Update rest lengths, in case they've changed due to cursors:
            for (int i = 0; i < renderers.Count; ++i)
            {
                var rope = renderers[i].actor as ObiRopeBase;

                for (int j = chunkOffsets[i]; j < chunkOffsets[i + 1]; ++j)
                {
                    var data = pathData[j];
                    data.restLength = rope.restLength;
                    pathData[j] = data;
                }
            }
        }
    }
}

