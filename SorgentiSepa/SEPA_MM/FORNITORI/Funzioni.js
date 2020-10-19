if (document.getElementById('CPContenuto_TextBox1')) {
    if ((document.getElementById('CPContenuto_TextBox1').value != '2') && (document.getElementById('CPContenuto_TextBox1').value != '1')) {
        NascondiDiv();
    } else {
        MostraDiv();
    }
    if (document.getElementById('CPContenuto_btnEliminaElemento')) {

        document.getElementById('CPContenuto_btnEliminaElemento').style.visibility = 'hidden';
        document.getElementById('CPContenuto_btnEliminaElemento').style.position = 'absolute';
        document.getElementById('CPContenuto_btnEliminaElemento').style.left = '-100px';
        document.getElementById('CPContenuto_btnEliminaElemento').style.display = 'none';
    }
}
else {
    if (document.getElementById('TextBox1')) {
        if ((document.getElementById('TextBox1').value != '2') && (document.getElementById('TextBox1').value != '1')) {
            NascondiDiv();
        } else {
            MostraDiv();
        }
        if (document.getElementById('btnEliminaElemento')) {
            document.getElementById('btnEliminaElemento').style.visibility = 'hidden';
            document.getElementById('btnEliminaElemento').style.position = 'absolute';
            document.getElementById('btnEliminaElemento').style.left = '-100px';
            document.getElementById('btnEliminaElemento').style.display = 'none';
        }
    }
}


function MostraDiv() {
    document.getElementById('divInsA').style.visibility = 'visible';
    document.getElementById('divInsB').style.visibility = 'visible';
    document.getElementById('divInsA').style.display = 'block';
    document.getElementById('divInsB').style.display = 'block';
    if (document.getElementById('NavigationMenu')) {
        document.getElementById('NavigationMenu').style.visibility = 'hidden';
    }

};

function NascondiDiv() {
    document.getElementById('divInsA').style.visibility = 'hidden';
    document.getElementById('divInsB').style.visibility = 'hidden';
}
function EliminaElemento() {
    if (document.getElementById('CPContenuto_LBLID')) {
        if (document.getElementById('CPContenuto_LBLID').value != '') {
            confirm('Attenzione', Messaggio.Elemento_Elimina, 'SI', 2, 'CPContenuto_btnEliminaElemento', 'NO', 3, '');
        }
        else {
            message('Attenzione', Messaggio.Elemento_No_Selezione);
        }
    }
    else {
        if (document.getElementById('LBLID')) {
            if (document.getElementById('LBLID').value != '') {
                confirm('Attenzione', Messaggio.Elemento_Elimina, 'SI', 2, 'btnEliminaElemento', 'NO', 3, '');
            }
            else {
                message('Attenzione', Messaggio.Elemento_No_Selezione);
            }
        }
    }
}