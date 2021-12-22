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

## `MonoBehavior` lifecycle quirks

Any `MonoBehavior` in Unity can have a number of magic "lifecycle" methods.
These are not actually overrides on `MonoBehavior`, but rather methods matching
certain keywords that Unity will (presumably) use C#'s reflection APIs to
discover and call them at the appropriate time.

In many game engines, you'll have some kind of update method that is supposed to
be called once per "tick" - where a tick is either a _rendered frame_ in the
game, or some _fixed_ tick that runs at a guaranteed rate.

In Unity, `MonoBehavior` looks for both `Update` _and_ `FixedUpdate` magic
keyword methods.

- `Update` is run once per _rendered frame_, which depends on the framerate the
  game can render at
- `FixedUpdate` is run once per _fixed timestep_, which is guaranteed to run at
  a constant rate

From my early days of gamedev, I learned the hard way to do all gameplay logic
within fixed timestep updates, so while learning Unity, I skipped `Update`
altogether and went straight to `FixedUpdate`.

I also took many tutorials that _don't_ use `FixedUpdate` - which I found
irritating/disappointing since it's so important to get this right early on, but
I digress...

These tutorials often use the `UnityEngine::Input` namespace to query
keys/buttons etc.

For convenience, Unity has `Input.ButtonDown`/`Input.KeyDown` methods that tell
you if a button has been pressed "this frame". **Emphasis on _frame_** - these
are **not** reset between `FixedUpdate` calls, so depending on your framerate,
these could be `true` any number of times in a row within `FixedUpdate`.

Therefore, if you must use these it's recommended to use them inside `Update`,
track any data about input in your state, and respond to it in `FixedUpdate`.

I ... really don't like this, but so many tutorials that _do_ use `FixedUpdate`
follow this pattern, so for now I'm following this pattern, but I'm going to
look into replacing or reconfiguring `Input` to play nicer with `FixedUpdate` to
avoid all this extra state management and to allow for (in theory) more precise
controls.
