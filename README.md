# Visualizing my raytracer from scratch in unity
The sole purpose of this project is to [visualize my first raytracer from scratch](https://github.com/j-2k/Raytracing) in a better prespective using a engine.<br>

## Playing The Project / Instructions

When in the game view (IN PLAYMODE) of the project press space to view the hit positions on the sphere & they should be displayed in the scene view.<br>
This was not made with a high focus on optimization but just optimized decently.<br>
You can also click the raytracer object in playmode & view the script attached to it. You can change many things such as the radius of the sphere, show rays, show hit positions, change alpha of rays that hit nothing & etc.<br> 
Project made in Unity > BIRP > Version 2021.3.16f1<br>
Here are all the parameters you can control, explanation is below the image.<br>
![Screenshot_2](https://user-images.githubusercontent.com/52252068/235283477-2f3b19af-5c39-4f65-967e-b2cfebe8b478.png)<br>
- Width & Height resemble the density of rays & is essentially just resolution (Aspect ratio taken into account).<br>
- Length is just simply the magnitude of the ray.<br>
- Radius is the radius of the sphere to raytrace (the white gizmo sphere).<br>
- Forward offset to move the ray origin on the Z axis. <br>
- Main Light can be anything but just takes its direction its pointing @ to calculate shading.<br>
- Root 0 Alpha is the color alpha of the rays not hitting the sphere 0 is nothing/transparent 1 is max/opaque. <br>
- The next 3 bools are to show rays, show the hit positions as gizmos, & showing the position of the raytraced sphere as a gizmo. <br>
- Reload Gizmos Key, Press the reload key to see gizmo hit positions, & always keep pressing it when changing values.<br>
- Finally, just gizmo sphere size.





The last 4 lists all dont need to be tampered with & are there for debugging purposes.



## Pull requests!

I also don't know of a method to draw gizmos in the update method if you know a good method tell me about it.<br>
I never found a "good way" of drawing gizmos dynamically (i did have a dynamic method but it was very laggy when increasing radius because of the # of gizmos that would need to be drawn on the screen.) so if you have a better method maybe do a pull req or tell me about it, thanks.<br>
<br>

Here are some gif & images below to see the project without downloading.
<p align="center">
  <img src="https://user-images.githubusercontent.com/52252068/233801093-89c26e6d-1ea6-4914-bac1-b0d917e13e3e.gif"/>
  <img src="https://user-images.githubusercontent.com/52252068/233801095-d5136fb1-d235-4bf3-ab5d-d59e79e1b356.png"/>
  <img src="https://user-images.githubusercontent.com/52252068/233801096-8ad70404-75bd-4a67-beb0-aad7a6af44a5.png"/>
  <img src="https://user-images.githubusercontent.com/52252068/233801097-abc14395-2f56-40bb-a5d9-95a229a70168.png"/>
  <img src="https://user-images.githubusercontent.com/52252068/233801098-9f627a15-4125-44a5-a73a-6ffd5f21e2c7.png"/>
</p>

