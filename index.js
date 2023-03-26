(async () => {

    const response = await fetch("https://localhost:44383/autor");
    const autores = await response.json();
    //document.body.innerHTML = JSON.stringify(autores, null, "\t");

    const rows = autores.map(a => {
        return `
            <tr>
                <td>${a.Id}</td>
                <td>${a.Nome}</td>
                <td>${a.Nascimento}</td>
                <td>${a.Falecimento ?? "---"}</td>
                <td>
                    <button>Editar</button>
                </td>
                <td>
                    <button>Remover</button>
                </td>
            </tr>
        `;
    });

    document.querySelector("table#autores tbody").insertAdjacentHTML("beforeend", rows.join(""));

})();