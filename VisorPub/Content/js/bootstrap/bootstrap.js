document.querySelectorAll("#cntBusqueda .panel-heading").forEach(tr => {
    tr.addEventListener('click', function () {
        //let sel = document.querySelector("#" + tableId + " tbody tr.seleccionado");
        //if (sel) {
        //    sel.classList.remove('seleccionado')
        //};

        //this.classList.add("seleccionado");

        //let pubId = this.firstElementChild.innerHTML;
        let id = this.getAttribute("data-target")

        if (this.classList.contains("collapsed")) {
            this.classList.remove('collapsed');
            document.getElementById(id).style.display = '';
        } else {
            this.classList.add('collapsed');
            document.getElementById(id).style.display = 'none';
        }

        //PublicationDetail2(pubId);
    });
});