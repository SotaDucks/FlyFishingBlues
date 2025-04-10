**************************************
*             BEAUTIFY               *
* Created by Ramiro Oliva (Kronnect) * 
*            README FILE             *
**************************************


How to use this asset
---------------------
We recommend importing the asset in an empty project and run the Demo Scenes provided to get an idea of the overall functionality.
Read the documentation and experiment with the tool.

Later, you can import the asset into your project and remove the demo folder.

Hint: to use the asset, select your camera and add "Beautify" script to it.


Documentation/API reference
---------------------------
Please check the Documentation folder. It contains additional instructions and description about the asset, as well as some recommendations.


License
-------
Beautify is licensed under the Unity Asset Store EULA or Kronnect EULA (depending where you purchased the asset).
Beautify also includes two optional custom ports of tonemap operators (ACES Fitted and AGX) which are licensed under the MIT license:
https://opensource.org/license/mit
Check the BeautifyACESFitted.hlsl and BeautifyAGX.hlsl files respectively. You can clear them if you don't use these tonemappers.    


Support
-------
Please read the documentation and browse/play with the demo scene and sample source code included before contacting us for support :-)

Have any question or issue?
* Support-Web: https://kronnect.com/support
* Support-Discord: https://discord.gg/EH2GMaM
* Email: contact@kronnect.com
* Twitter: @Kronnect



Future updates
--------------

All our assets follow an incremental development process by which a few beta releases are published on our support forum (kronnect.com).
We encourage you to signup and engage our forum. The forum is the primary support and feature discussions medium.

Of course, all updates of Beautify will be eventually available on the Asset Store.



Version history
---------------

Version 22.0.2
- [Fix] Fixed anamorphic flares layer mask not applied through profiles

Version 22.0.1
- [Fix] Fixed chromatic aberration not enabled correctly in Beautify inspector

Version 22.0
- Added AGX tonemap operator

Version 21.3
- Improved VR state detection

Version 11.4
- Outline: added Downscale Blur option which can be disabled to increase outline quality/sharpness
- Outline: added Min Depth Threshold option

Version 11.3
- Depth of field: added "Cull Mode" option to exclusion/transparency sections

Version 11.2
- ACES: added pre-exposure/post-brightness options

Version 11.1
- Bloom improvements
- Added "Antiflicker Max Output" option. Can be used along intensity and threshold to improve control over fireflies/flickering

Version 11.0
- Bloom: improved quality of bloom in best performance mode
- Bloom: added Iterations option to fine-tune the spread of bloom in best performance mode
- Bloom: added individual tint color options per each layer in best quality mode for improved artistic control when bloom compose option is enabled

Version 10.8
- Sun Flares: added Radial Offset parameter
- Anamorphic flares: streak length is now aspect ratio independent

Version 10.7
- Bloom: improved stability in Best Performance mode
- Bloom: Ultra option is now available in Best Performance mode
- Faster dither when depth based effects option is disabled
- Downscale/supersampling is now a fractional value
- [Fix] Fixed downscale/supersampling on Android in Unity 2022.3

Version 10.6
- Anamorphic flares: prevents using too large render textures in very high resolutions

Version 10.5
- Added new Compare Mode option (vertical line)

Version 10.4.2
- [Fix] Fixed super-sampling and pixelate with downscale in Unity 2022

Version 10.4.1
- [Fix] Fixed "Customize Bloom" issue

Version 10.4
- Added "Conservative" threshold option to bloom and anamorphic flares
- [Fix] Fix to a bug in Unity 2022.2 related to blit material
- [Fix] Fixed an issue with layer mask 31 in culling mask options

Version 10.3
- [Fix] Fixed image aspect ratio when using supersampling and custom viewport size

Version 10.2
- Added "Automatic" command in Shader Options. This is a convenient way to quickly toggle shader features based on current used Beautify options.
- Optimization: removed one full screen blit when using super-sampling or pixelate/downscale.
- Antialias: added "Max Spread" option.

Version 10.1
- Added "Motion Restore Speed". Improved accuracy of motion sensibility.
- Antialias: added "Depth Attenuation". Reduces antialias effect on distance

Version 9.1
- Frame: added cinematic bands option
- Bloom: uncapped "Depth Attenuation" limit

Version 9.0
- Option to use ACES tonemapping in Best Performance mode

Version 8.8.1
- LUT Browser fixes
- Improved compatibility with Unity 2021.3

Version 8.8
- Added new compare mode options
- Added 3D LUT Cube format support

Version 8.7
- Minimum Unity version required is now 2019.4.13
- Depth of field: added real camera settings
- Removed global shader keywords

Version 8.6
- Added a new advanced parameter "Kill NAN" to remove invalid pixels from the input image
- [Fix] Fixed scene being marked as unsaved when depth of field distance debug value was updated
- [Fix] VR: fixed bloom layer mask issue with Single Pass Instanced

Version 8.5
- Added the ability to use different layer masks for bloom and anamorphic flares

Version 8.4
- Added Frame Pack browser

Version 8.3.1
- Reduced number of shader variants
- Improved LUT browser UI
- [Fix] Fixed depth of field bokeh threshold in gamma color space

Version 8.3
- Added "Composition" option to depth of field in high quality mode

Version 8.2
- Added LUT Browser (access it from inspector or from Window -> LUT Browser)
- Added "Pure LUT" button to inspector which resets all other color grading options

Version 8.1.1
- Added Chromatic Aberration effect
- Beautify can now be compiled without any reference to VR/XR modules (check documentation)
- [Fix] Depth threshold was visible when advanced option "Depth-based effect" was disabled

Version 8.0
- Added new options for outline effect
- Spotlight can be assigned to Sun flare
- DoF: added blur spread option when foreground HQ blur is enabled

Versino 7.9.1 21/Dec/2021
- Added "Sun Flares Reveal Speed" / "Sun Flares Hide Speed" (works only if depth checking option is not used)
- Shader material handling optimizations

Version 7.9
- Added "Vignetting Center" parameter
- Added "Blink Style" (cutscene or human style)
- Added "Bloom Near Attenuation Distance" parameter
- Minor change to support shader options updates when only the mobile-version shader is kept

Version 7.8
- Added "Hard Light" effect (enable it in Shader Options)
- New option "Depth Based Effects" in Shader Options. Disable to improve performance on mobile.

Version 7.7
- Support for LUTs of different resolutions than standard 1024x32
- Added Eye Adaptation option: "Show In Editor"

Version 7.6
- Added bloom color tint option
- Added option to detect Sun occlusion using sky color chroma
- [Fix] Fixed issue with the anamorphic flares resolution slider

Version 7.5
- Added Super Sampling option in Best Quality mode
- [Fix] Fixed dof autofocus distance to reflect exact distance between camera and focus target

Version 7.3.1
- [Fix] Fixed Sun flares rendering issue with Single Pass Stereo on Oculus Quest

Version 7.3
- Added "Quality" slider when Ultra setting is enabled
- Added "Camera Event" option under Bloom/DoF Layer Mask

Version 7.2
- Added "Sync with Profile": when disabled, you can change inspector settings and keep those modifications regardless of active profile (click 'Load' to reload profile settings). When enabled (default), profile settings will replace inspector settings when entering playmode.
- [Fix] Fixed issue in VR when bloom layer mask is enabled in Oculus Go

Version 7.1.1
- Added Downscale option on top of inspector when in Best Performance mode (previously this option could be found under pixelate effect)

Version 7.1
- Bloom: added "Quicker Blur" to bloom effect in High Quality mode
- Sharpen: added Depth FallOff to smooth effect around min/max depth limits
- Added "Depth Based Sharpen" option in Shader Options menu. Disable if don't need min/max depth control to improve performance a bit.
- Improved Depth of Field effect: reduced "popping" effect when changing focus distance

Version 7.0.2
- [Fix] Fixed issue with changing bloom culling mask not refreshing immediately
- [Fix] Fixed issee when enabling Sun depth-based occlusion option 
- [Fix] Fixed Sun flares issue with Single Pass Stereo (Unity 2018+)

Version 7.0.1
- Added LUT import settings check to Inspector. Shows a warning and button to auto-fix if necessary
- Bloom layer mask is now ignored if it's set to Everything (equals to not using layer mask)
- [Fix] Fixed LUT issue in linear color space

Version 7.0
- DoF: added depth of field transparency layer mask option
- DoF: added OnBeforeFocus event to allow user defined focus distance calculation
- DoF: added Max Distance parameter
- DoF: added Viewport point parameter for autofocus mode
- DoF: current focal point distance is now visible in the inspector
- Exposure now accepts values greater than 2 in HDR mode
- Core Effects option in Shader Options now split between Sharpen and Color Tweaks
- Added basic AntiAlias option in Best Quality mode
- New anti-alias depth threshold option
- [Fix] Fixed issue with camera viewport and bloom/dof exclusion masks
- [Fix] Workaround for depth bug with Oculus GO and stereo rendering
- [Fix] Fixed pixelate error in Unity 2017.2+ with Best Performance mode

Version 6.2
- Depth of Field: added High Quality option to Foreground blur
- Added Better Fast LUT in Shader Options
- Editor & standalone optimizations

Version 6.1.1
- [Fix] Fixed profile not preserving changes between sessions when hitting "Apply" from Beautify's inspector

Version 6.1
- Added Vignetting Aspect Ratio
- Added Vignetting Blink effect
- Depth of field: added min/max autofocus distance
- Clicking "Apply" to save profile changes will refresh automatically other cameras using same profile
- [Fix] Fixed Metal issue with Sun flares on Unity 2018.1

Version 6.0
- Added LUT support
- Added Pixelate effect with downsampling option
- Added Vignetting Fade Out effect

Version 5.6
- Renamed and expanded Build Options to include new shader options
- Custom bloom & anamorphic flares blur handling for Mali-T720 GPU
- Performance improvements
- Change: Outline Sobel setting is now a shader advanced option

Version 5.5.2
- Added new shader option to enable Sun flares occlusion with objects that don't have colliders (see manual)
- Added new tonemap operator which produces less saturation on bright colors (see manual)
- [Fix] Fixed issue with Sun flares not showing up when bloom and anamorphic flares are not used
- [Fix] Fixed bloom blur aspect ratio bug in best performace mode
- [Fix] Automatic fix for Mali-T720 bug with floating point render textures

Version 5.5.1
- [Fix] Fixed vignetting mask not respecting fully transparent areas
- [Fix] Fixed a compilation issue in BeautifyMobile.cginc for Unity 2017.2+ version

Version 5.5
- VR: added support for VR Single Pass Instanced mode
- Sun Flares: added occlusion mask option
- Eye Adaptation: improved algorithm in Best Quality setting
- Anamorphic Flares: added option to remove blur passes with Best Performance mode
- [Fix] Added shader workaround for Mali G71 GPU hardware issue with normals
- [Fix] Fixed bloom/flares temporarily disappearing when saving the scene

Version 5.4.1
- Improved blur transition

Version 5.4
- New Blur effect: adds a screen blur effect with customizable intensity
- Added Depth Of Field Filter Mode option to control potential artifacts when using exclusion layer mask and/or downsampling
- [Fix] Selecting "Basic" quality setting no longer change extra effects settings

Version 5.3
- Profiles. Create, reuse and share persistent settings profiles
- Changed "Blurred" bloom preset to make it more blurry
- Dithering moved to the end of effect stack to affect bloom and other effects
- [Fix] Under-the-hood inspector improvemeents (undo now works, standard slider behaviour, ...)
- [Fix] Removed some artifacts with DoF Exclusion layer option when MSAA is enabled

Version 5.2.2
- Depth of Field: added max brightness clamp to prevent black artifacts with out of range HDR pixels
- [Fix] Now changing inspector properties will mark the scene as modified
- [Fix] VR: compare mode now works in VR (also SPSR)

Version 5.2.1
- New demo scene 5: particle + bloom layer mask example
- Added Mask Z Bias option to bloom layer mask (enables blooming behind opaque objects)
- [Fix] Fixed bloom layer mask clipping issue with transparent objects
- [Fix] Fixed banding issue with Unity 2017.1 on Android platform

Version 5.2
- VR: Support for Single Pass Stereo Rendering changes in Unity 2017.1
- Added downsampling options to Bloom Culling Mask, Depth of Field Exclusion Mask and Depth of Field Transparent Mask features.
- Added "Boost" options to bloom layers
- Added Max Bright parameter to prevent out of range HDR issues with Bloom and to clamp high luminosity pixels

Version 5.1
- Basic mode no longer activates Depth buffer generation which may save performance in forward rendering path
- Added inspector warning when setting bloom layer mask to everything.
- [Fix] Fixed bloom layer mask issue with Lens Dirt

Version 5.0
- Support for orthographic camera
- Added Rotation DeadZone to Sun Flares
- Added DoF exclusion layer bias parameter
- [Fix] Fixed bloom layer mask issue with VR
- [Fix] Fixed pink issue with DoF exclusion layer and water/reflections shaders

Version 4.5
- Added Sun Flares Tint and Solar Wind Speed options
- Added Bloom layer mask

Version 4.4.1
- [Fix] Fixed black out issue on certain scenes with some pixels out of HDR range
- [Fix] Fixed Depth Of Field grain effect on skybox

Version 4.4
- New feature: Sun Flares! Beautiful, fast and procedural lens flares.
- Added bloom depth atten (reduces bloom effect with distance, useful to cancel bloom on skybox)
- Added bloom & anamorphic flares ultra mode which improves quality especially for VR
- Added full support for Unity Animator window
- Added Basic mode with a super-reduced/fast feature set
- Added Depth of Field exclusion mask
- Changed Outline order so it renders before bloom
- [Fix] Optimization: prevented execution of some script code associated with disabled effects

Version 4.3.2
- Added bloom blur option for Best Performance mode
- Reduced shader keyword usage in compare mode pass
- Texture fetch optimizations for the Best Performance mode
- Vignette Mask and Frame Mask come now disabled by default (to use them, re-enable in Build Options)
- [Fix] Removed unharmful console warnings on Unity 5.6

Version 4.3.1
- [Fix] Fixed shader error when enabling night or thermal vision plus outline

Version 4.3
- Added Outline Sobel method
- Reduced shader keywords by one

Version 4.2.1
- Fixed Eye Adaptation error with Single Pass Stereo Rendering
- Added reminder in inspector to use build options

Version 4.2
- New mask texture support for vignetting and frame effects
- Fixed depth of field transparency issue with antialias on DX11
- Minor internal improvements

Version 4.1.3
- Fixed issue with Unity 5.4 and Beautify attached to prefab

Version 4.1.2
- Fixed Single Pass Stereo Rendering on some configurations

Version 4.1.1
- Added support for Unity 5.5
- Fixed lens dirt effect not working correctly when bloom and anamorphic flares is enabled

Version 4.1
- Added transparency support to Depth of Field effect
- Improved bokeh effect with option to enable/disable it

Version 4.0
- Added ACES tonemap operator
- Added eye adaptation effect
- Added purkinje effect (achromatic vision in the dark + spectrum shift)
- New build options to optimize compilation time and build size
- Better bloom & anamorphic flares when using best performance setting
- Added layer mask field to depth of field autofocus option
- Fixed anamorphic flares vertical spread using incorrect aspect ratio

Version 3.2.1
- Fixed depth of field goint to full blur strength when looking aside an assigned target focus
- Fixed depth of field shader unroll issue

Version 3.2
- Added autofocus option to depth of field
- Fixed depth of field affecting scene camera

Version 3.1
- Added depth of field
- Fixed daltonize filter issue with pure black pixels

Version 3.0.0
- Added anamorphic flares!
- Added sepia intensity slider
- Improved bloom performance

Version 2.4.1
- Demo folder resources renamed to DemoSources to prevent accidental inclusion in builds
- Fixed issue when changing Beautify properties using scripting from Awake event

Version 2.4
- Improved support for 2D / orthographic camera

Version 2.3
- Added vignetting circular shape option
- Improved bloom effect in gamma color space and mobile
- Added 3 new lens dirt textures

Version 2.2.2
- Fixed compare mode with DX/Antialias enabled
- Throttled sharpen presets in linear color space

Version 2.2.1
- Effect in Scene View now updates correctly in Unity 5.4

Version 2.2
- VR: Experimental Single Pass Stereo Rendering support for VR (Unity 5.4)
- Effect now shows in scene view in Unity 5.4
- New Compare Mode options

Version 2.1
- Bloom antiflicker filter

Version 2.0
- Redesigned inspector
- New extra effects

Version 1.0
- Initial Release






