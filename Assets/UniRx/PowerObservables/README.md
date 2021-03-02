# [BLOG](https://github.com/GbrosGames/Blog) - [UniRx](https://github.com/neuecc/UniRx) - PowerObservables

Usefull UniRx tools and Blog post resources.

## Package Installation 

Since unity doesn't support git dependencies in package.json you have to [install UniRx](https://github.com/neuecc/UniRx#upm-package) manually. 

Edit manifest.json file in your Unity Packages directory 


```
{
  "dependencies": {
    "com.gbros.blog.unirx.powerobservables": "https://github.com/GbrosGames/Tools.git?path=Assets/UniRx/PowerObservables",
    "com.neuecc.unirx": "https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts"
}
```

Or [install](https://docs.unity3d.com/2020.2/Documentation/Manual/upm-ui-giturl.html) via url

```
https://github.com/GbrosGames/Tools.git?path=Assets/UniRx/PowerObservables
```
```
https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts
```


## Examples

You can import examples inside UnityPackage Menager > Gbros - UniRx - PowerObservables
