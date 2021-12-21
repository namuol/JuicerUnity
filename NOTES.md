# Notes

Internal notes to help me remember useful commands, tricks, etc.

## Git LFS

I followed [this YouTube tutorial](https://www.youtube.com/watch?v=09McJ2NL7YM).

### Unity settings for git usage

Ensure that `Project Settings` -> `Editor` -> `Asset Serialization` -> `Mode` is
set to `Force Text` (This was the default for me).

### Git LFS Commands

To install it on MacOS:

```
brew install git-lfs
```

To use it in a git repo:

```
git lfs install
```

To track files with a certain extension with git LFS:

```
# e.g. blender files:
git lfs track "*.blend"
```

This will create a `.gitattributes` file if it doesn't already exist, and will
update it otherwise.

