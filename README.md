# Unity3D Rewind
Rewind time in Unity3D

Currently a prototype. It 'records' `position`, `rotation` and `scale` of registered objects every frame and lets player 'rewind' time using that data.

* The `TimedObject` component needs to be assigned to a `GameObject` for it to be compatibile with the time manipulation.
* You can also record `Rigidbody`'s velocity by adding the rigidbody component to an object, but it's not required.

Feel free to use it in your projects/modify/update; basically do whatever you want with it.