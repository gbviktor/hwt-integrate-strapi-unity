# hwt-integrate-strapi-unity
Maybe simplest way to communicate with Strapi from Unity, go, just try it.

## Install via Unity Package Manager

![image](https://user-images.githubusercontent.com/46207/79450714-3aadd100-8020-11ea-8aae-b8d87fc4d7be.png)

```
https://github.com/gbviktor/hwt-integrate-strapi-unity.git
```

## Usage

#### Get Repository and All Items
```
//MyVector3 is a example model with interface implementation IStrapiEntityType
//1.Generate token in Strapi

var strapi = new Strapi("http://localhost:1337","Bearer ...");

//api/position - is a endpoint on strapi
var promocodes = strapi.CreateRepository<MyVector3>("api/position");
List<MyVector3> res = promocodes.GetAll();
```
[in dev]
