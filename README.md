![image](https://user-images.githubusercontent.com/1713032/170317085-4d209b94-6d7b-44d9-b643-1d0c8c45c1b5.png)

# Collapse Test Project

This project is a simplistic version of a collapse game.

- Built in Unity 2020.3.x.
- The game will be tested only inside the unity editor, don't worry about anything else.
- Please create a new project from this template (Find the green _"use this template"_ button on this page) and complete the tasks below, send us a link to the repo once you're done.
- Most of the basic functionality is already coded so this test can focus on quality over quantity.
- Ask for hints if you're stuck.

## Tasks:

1. The file _BoardManagerInteractionLogic.cs_ contains an unimplemented critical function _FindChainRecursive_ - please implement it using recursion, see code comments for more info.

2. Now that clicking blocks makes whole groups match and disappear correctly we can see that the blocks disappear instantaneously and that's not the experience we're after - please improve the visuals of the disappearance of blocks, something cute and simple will do the trick. (Hint: the project is making heavy use of DOTween)

3. As you can probably tell the Bombs don't really work, please find the _Bomb_ class and make them work:
   - Clicking a Bomb should cause it to shake and trigger all blocks around it (in all 9 directions)
   - Bonus points if Bomb explosions are delayed and not happen all at once, in order to create a fun chain effect. Board regeneration should be done only after all Bombs are exploded.

## TODOs:

1. Use Unity's ObjectPool util to make all instantiated objects be object pooled instead of instantiated and created all the time (will save mem allocation on low end devices).
2. remove all unnecessary comments/logs.
