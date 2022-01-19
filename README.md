# unity-ui-ex
UI extension based on Unity UI system.

# install
Use this repository directly in Unity.

### Dependencies
* https://github.com/KleinerHacker/unity-extension
* https://github.com/KleinerHacker/unity-animation

### Open UPM
URL: https://package.openupm.com

Scope: org.pcsoft

# usage

### Components
* `UIDiaShow` - Show multiple images animated
* `UIList` - Basic class to implement a simple list
* `UIListItem` - Basic class to implement a simple list item
* `UIProgressBar` - A simple progress bar with two options:
  * Based on a slider
  * Based on a filled image
* `UIProgressIndicator` - A simple indicator that animate a rotation only

### Windows
* `UIDialog` - A simple dialog
* `UINotification` - A simple temporary shown notification

### Hover System
You can find the hover settings in projects settings. There are default settings and specific settings identified by a key.
* Scale - Setup min and max values for UI scaling dependend on min and max distance values.
* Alpha - Setup min and max values for UI alpha (`CanvasGroup`) depend on min and max distance values.
* Interpolation - Scale and Alpha have an interpolation curve to evaluate changes of values by distance.

The last action is to add `UIHoverPanel` component to your UI object, setup the target transform and the camera to use (or default the main camera). Than this element follows the 3D transformation point with all settings of Alpha and Scaling behavior.

### Input
* `UIButtonInput` - Direct Input for a Button
* `UIToggleInput` - Direct Input for a Toggle
* `UIToggleGroupInput` - Direct Input for a complete Toggle Group
* `UISliderInput` - Direct Input for a Slider
* `UIDropdownInput` - Direct Input for Dropdowns
* `UIScrollViewInput` - Direct Input for Scroll Views

Setup via Project Settings.

### Audio
* `UIButtonJingle` - Play a jingle on button click
* `UIToggleJingle` - Play a jingle on toggle changed
* `IOSliderJingle` - Play a jingle on slider value changed

The audio settings can changed in project settings. You can switch to SFX system.
