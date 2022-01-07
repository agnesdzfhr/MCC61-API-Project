

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

            } else if (val.base_stat > 50 && val.base_stat < 76) {
                pokeStats += `
                            <div class="col">
                                <div>${val.stat.name}</div>
                            </div>
                            <div class="col">
                                <div class="progress mt-2">
                                    <div class="progress-bar bg-info" role="progressbar" style="width: ${val.base_stat}%;" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
                                </div>
                            </div>`;
            } else if (val.base_stat > 25 && val.base_stat < 51) {
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

$(document).ready(function () {
    $('#tPoke').DataTable();
});

