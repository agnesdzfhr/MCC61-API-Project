// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//var data = document.getElementById("judul");
//data.addEventListener("click", function () {
//    data3.style.backgroundColor = "blue";
//});

//var data3 = document.querySelector("section#a p.p2");
//var d3 = data3.innerHTML;
/*var data = document.querySelector("section#a p.p2");*/

//var data2 = document.getElementByClassName('list');
//for (var i = 0; i < data2.length; i++) {
//    var d2 = data2[i].style.backroundColor = "lightgreen";
//}


//var data4 = document.querySelectorAll(".list");

//var data5 = $(".list").html("halo ini diubah melalui jquery");
//$.each(data5)
var kpopData = document.getElementById("button-kpop");
var kdramaData = document.getElementById("button-kdrama");
var kfoodData = document.getElementById("button-kfood");

function kpopFunction() {
    kpopData.classList.add("active");
    kdramaData.classList.remove("active");
    kfoodData.classList.remove("active");
    document.getElementById("sub-judul").innerHTML = "K-팝: Korean Pop";
    document.getElementById("image").src = "https://i.pinimg.com/564x/68/48/c2/6848c277b2df9cd2b930bd372b1f78d7.jpg";
    document.getElementsByClassName("pr1")[0].innerHTML = "Korean popular music, or most commonly known as K-pop is mainstream music that originated from South Korea.K - pop music in the country could be thought of as similar to Taylor Swift in the United States."
}

function kdramaFunction() {
    kpopData.classList.remove("active");
    kdramaData.classList.add("active");
    kfoodData.classList.remove("active");
    document.getElementById("sub-judul").innerHTML = "K-드라마: Korean Drama";
    document.getElementById("image").src = "https://vuclipi-a.akamaihd.net/p/cloudinary/h_171,w_304,dpr_1.5,f_auto,c_thumb,q_auto:low/1165060405/d-1";
    document.getElementsByClassName("pr1")[0].innerHTML = "Korean drama or K-drama refers to televised dramas in Korean language, made in South Korea, mostly in a miniseries format, with distinctive features that set it apart from regular Western television series or soap operas."
}

function kfoodFunction() {
    kpopData.classList.remove("active");
    kdramaData.classList.remove("active");
    kfoodData.classList.add("active");
    document.getElementById("sub-judul").innerHTML = "K-음식: Korean Food";
    document.getElementById("image").src = "https://i.pinimg.com/236x/d1/1e/b1/d11eb154618cbf880d49a78d2d5ed592.jpg";
    document.getElementsByClassName("pr1")[0].innerHTML = "Korean food is mostly made up of rice, noodles, vegetables, and meats. Most Korean meals have many side dishes (called banchan) along with their steam-cooked rice. Kimchi is usually eaten at every meal. Sesame oil, doenjang, soy sauce, salt, garlic, ginger, pepper and gochujang are ingredients that are often used in the food."
}

//array of object
const animals = [
    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
]

let onlyCat = [];

for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        onlyCat.push(animals[i]);
    }

    if (animals[i].species == "snail") {
        animals[i].kelas.name = "Non-Mamalia";
    }
}

console.log(onlyCat);
console.log(animals);




