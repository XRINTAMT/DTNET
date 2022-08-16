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

1. Add a OutlineNormalsCalculator component to an object.
2. Set GameObject layer to "Outlined".
3. Find this object's mesh and set "Read/write enabled" to True.

## External assets


PathCreator: https://assetstore.unity.com/packages/tools/utilities/b-zier-path-creator-136082
Spline mesh: https://assetstore.unity.com/packages/tools/modeling/splinemesh-104989


If adding libraries or packages from the Unity asset store add them here.
