// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*const { error } = require("jquery");*/

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
//var kpopData = document.getElementById("button-kpop");
//var kdramaData = document.getElementById("button-kdrama");
//var kfoodData = document.getElementById("button-kfood");

//function kpopFunction() {
//    kpopData.classList.add("active");
//    kdramaData.classList.remove("active");
//    kfoodData.classList.remove("active");
//    document.getElementById("sub-judul").innerHTML = "K-팝: Korean Pop";
//    document.getElementById("image").src = "https://i.pinimg.com/564x/68/48/c2/6848c277b2df9cd2b930bd372b1f78d7.jpg";
//    document.getElementsByClassName("pr1")[0].innerHTML = "Korean popular music, or most commonly known as K-pop is mainstream music that originated from South Korea.K - pop music in the country could be thought of as similar to Taylor Swift in the United States."
//}

//function kdramaFunction() {
//    kpopData.classList.remove("active");
//    kdramaData.classList.add("active");
//    kfoodData.classList.remove("active");
//    document.getElementById("sub-judul").innerHTML = "K-드라마: Korean Drama";
//    document.getElementById("image").src = "https://vuclipi-a.akamaihd.net/p/cloudinary/h_171,w_304,dpr_1.5,f_auto,c_thumb,q_auto:low/1165060405/d-1";
//    document.getElementsByClassName("pr1")[0].innerHTML = "Korean drama or K-drama refers to televised dramas in Korean language, made in South Korea, mostly in a miniseries format, with distinctive features that set it apart from regular Western television series or soap operas."
//}

//function kfoodFunction() {
//    kpopData.classList.remove("active");
//    kdramaData.classList.remove("active");
//    kfoodData.classList.add("active");
//    document.getElementById("sub-judul").innerHTML = "K-음식: Korean Food";
//    document.getElementById("image").src = "https://i.pinimg.com/236x/d1/1e/b1/d11eb154618cbf880d49a78d2d5ed592.jpg";
//    document.getElementsByClassName("pr1")[0].innerHTML = "Korean food is mostly made up of rice, noodles, vegetables, and meats. Most Korean meals have many side dishes (called banchan) along with their steam-cooked rice. Kimchi is usually eaten at every meal. Sesame oil, doenjang, soy sauce, salt, garlic, ginger, pepper and gochujang are ingredients that are often used in the food."
//}

////array of object
//const animals = [
//    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
//    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
//    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
//    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
//    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
//]


//let onlyCat = [];

//for (var i = 0; i < animals.length; i++) {
//    if (animals[i].species == "cat") {
//        onlyCat.push(animals[i]);
//    }

//    if (animals[i].species == "snail") {
//        animals[i].kelas.name = "Non-Mamalia";
//    }
//}

//console.log(onlyCat);
//console.log(animals);


//var data = document.getElementById("c1");

//function colorChange1() {
//    data.style.backgroundColor = "aqua";
//}

//ajax
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result.results);
    var text = "";
    $.each(result.results, function (key, val) {
        text += `<tr>
                    <td>${key+1}</td>
                    <td>${val.name}</td>
                    <td>
                        <button data-toggle="modal" data-target="#modalPoke" class="btn btn-primary" onclick="getDetails('${val.url}')">Detail</button>
                    </td>
                </tr>`;
    });
    console.log(text);
    $(".tablePoke").html(text);

}).fail((error) => {
    console.log(error);
})

function getDetails(url) {
    console.log(url);
    $.ajax({
        url: url
    }).done((result) => {
        console.log(result.name);
        var img = result.sprites.other.home.front_default;
        console.log(result);
        $("div.modal img#pokeImage").attr("src", img);

        var name = `<h3 class="text-capitalize font-weight-bold">${result.name}</h3>`;
        $(".pokeName").html(name);

        var detailType = "";
        $.each(result.types, function (key, val) {
            console.log(result.types[key].type.name);
            detailType += typeColor(val.type.name);
        })
        $("#pokeType").html(detailType);

        function typeColor(val) {

            if (val == "normal" || val == "flying" || val == "ground" || val == "water" || val == "grass" || val == "psychic" || val == "ice" || val=="fairy") {
                var color = `<span class="badge badge-pill badge-warning mr-2">${val}</span>`;
                return color;
            } else if (val == "fighting" || val == "electric" || val == "poison" || val=="rock" || val=="bug" || val=="steel" || val =="fire" || val=="dragon") {
                var color = `<span class="badge badge-pill badge-danger mr-2">${val}</span>`;
                return color;
            } else if (val=="ghost"||val=="dark" || val=="shadow" || val=="unknown") {
                var color = `<span class="badge badge-pill badge-dark mr-2">${val}</span>`;
                return color;
            }
        }

        var pokeAbl = "";
        $.each(result.abilities, function (key, val) {
            pokeAbl += `<div class="ml-3" style="display:inline">${val.ability.name}</div>`;
        })
        $("#pokeAbl").html(pokeAbl);

        var pokeStats = '';
        $.each(result.stats, function (key, val) {
            console.log(val);
            if (val.base_stat > 75) {
                pokeStats += `
                            <div class="col">
                                <div>${val.stat.name}</div>
                            </div>
                            <div class="col">
                                <div class="progress mt-2">
                                    <div class="progress-bar bg-success" role="progressbar" style="width: ${val.base_stat}%;" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
                                </div>
                            </div>`;

            } else if (val.base_stat > 50 && val.base_stat < 75) {
                pokeStats += `
                            <div class="col">
                                <div>${val.stat.name}</div>
                            </div>
                            <div class="col">
                                <div class="progress mt-2">
                                    <div class="progress-bar bg-info" role="progressbar" style="width: ${val.base_stat}%;" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
                                </div>
                            </div>`;
            } else if (val.base_stat > 25 && val.base_stat < 50) {
                pokeStats += `
                            <div class="col">
                                <div>${val.stat.name}</div>
                            </div>
                            <div class="col">
                                <div class="progress mt-2">
                                    <div class="progress-bar bg-warning" role="progressbar" style="width: ${val.base_stat}%;" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
                                </div>
                            </div>`;
            } else {
                pokeStats += `
                            <div class="col">
                                <div>${val.stat.name}</div>
                            </div>
                            <div class="col">
                                <div class="progress mt-2">
                                    <div class="progress-bar bg-danger" role="progressbar" style="width: ${val.base_stat}%;" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
                                </div>
                            </div>`;
            }

        });

        $(".stats").html(pokeStats);

    }).fail((error) => {
        console.log(error);
    })



}

