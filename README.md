# DTNET
DTNET VR app branch.

## Git workflow

### Setup

Install git LFS (https://git-lfs.github.com/). IMPORTANT: Also do the first step of ”Getting Started” run the command ”git lfs install” after installation to make sure it’s activated.

### Basics

Keep folder names in english

### Features and branches

For a new task start a new feature branch named "feature-*feature_name*" forked from the *main* branch. If finishing the feature takes multiple days I recommend merging from the *main* branch into your feature branch each day to make the eventual merging into main simpler.

Keep the feature branch up to date. Push changes even if they are not complete. There is no requirement that feature branches should be in a functioning state before they are completed.

Once a feature is complete move task to the Review column om Monday and submit a merge request on github.

### How to add highlights to objects 
1. Choose the object
2. Find commponent "Graddable" on it
3. Choose any highlight material in this component

### Scenario 2 TTS settings
The tts product used for the job: https://replicastudios.com/

| ID           | Character name (DTNET) | Voice name (Replica) |
|--------------|------------------------|----------------------|
| S2P1         | Elisa Johnson          | Harriet              |
| S2P2         | Helen Stewards         | Sorine               |
| S2P3         | Robert Johnson         | Gray                 |
| S2P4         | Michael Rosemary       | Sean                 |
| Female Nurse | Marite                 | Amber                |
| Male Nurse   | Alexander              |                      |

## External assets

#Paid (one would need to request a seat)

AutoHand: https://assetstore.unity.com/packages/tools/game-toolkits/auto-hand-vr-physics-interaction-165323
//Add scripts that require manual replacement to gitignore or at least have them uploaded in the cloud and put a link here.

Hospital props: https://assetstore.unity.com/packages/3d/props/hospital-props-vol-1-6-mega-pack-178128

Note: unlike hospital props, which is distributed per company (with unlimited seats), AutoHand is distributed per-seat, so the company would need to either re-assign the existing seats or buy some new ones to let anyone else run/build the project.

#Free

[Obsolete!] Spline mesh: https://assetstore.unity.com/packages/tools/modeling/splinemesh-104989

Bézier Path Creator: https://assetstore.unity.com/packages/tools/utilities/b-zier-path-creator-136082

Photon: https://assetstore.unity.com/packages/tools/network/pun-2-free-119922

Analog clock: https://assetstore.unity.com/packages/3d/props/interior/clock-4250

For outline urp shaders: https://github.com/Arvtesh/UnityFx.Outline

URP: is in the package manager by default.
//Just installing the URP does not solve the following issue: "UniversalRenderPipelineAsset_Renderer is missing RendererFeatures". If I'm not mistaken, it has something to do with the outline feature. 

If adding libraries or packages from the Unity asset store add them here.

// One would also need some of the default XR plugins for the controllers in the tutorial to show correctly, so, if you have it, please add the exact name here.
