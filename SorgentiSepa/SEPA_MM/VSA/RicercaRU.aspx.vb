
Partial Class VSA_RicercaRU
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try



                'QUERY = "select DIMENSIONI.VALORE AS NETTA,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,UNITA_CONTRATTUALE.CAP,UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE,rapporti_utenza.cod_contratto,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,unita_contrattuale.indirizzo,unita_contrattuale.civico,unita_contrattuale.cap,unita_contrattuale.interno,unita_contrattuale.scala,anagrafica.cognome,anagrafica.nome,anagrafica.cod_fiscale,soggetti_contrattuali.cod_tipologia_occupante,COMUNI_NAZIONI.NOME AS COMUNE  from SISCOM_MI.DIMENSIONI,SISCOM_MI.TIPO_LIVELLO_PIANO,COMUNI_NAZIONI,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where UNITA_CONTRATTUALE.ID_UNITA=DIMENSIONI.ID_UNITA_IMMOBILIARE (+) AND DIMENSIONI.COD_TIPOLOGIA='SUP_NETTA' AND UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO=TIPO_LIVELLO_PIANO.COD AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND substr(rapporti_utenza.cod_contratto,1,6)<>'000000' and UNITA_CONTRATTUALE.TIPOLOGIA='AL' AND rapporti_utenza.id=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and anagrafica.id=soggetti_contrattuali.id_anagrafica and rapporti_utenza.id=soggetti_contrattuali.id_contratto and anagrafica.cod_fiscale in (select cod_fiscale from comp_nucleo_VSA where id_dichiarazione=" & Request.QueryString("ID") & ")"
                QUERY = "select UNITA_CONTRATTUALE.NUM_VANI, DIMENSIONI.VALORE AS NETTA,UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AS PIANO,UNITA_CONTRATTUALE.CAP,UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE,rapporti_utenza.cod_contratto,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,unita_contrattuale.indirizzo,unita_contrattuale.civico,unita_contrattuale.cap,unita_contrattuale.interno,unita_contrattuale.scala,anagrafica.cognome,anagrafica.nome,anagrafica.cod_fiscale,soggetti_contrattuali.cod_tipologia_occupante,COMUNI_NAZIONI.NOME AS COMUNE  from SISCOM_MI.DIMENSIONI,COMUNI_NAZIONI,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where UNITA_CONTRATTUALE.ID_UNITA=DIMENSIONI.ID_UNITA_IMMOBILIARE (+) AND DIMENSIONI.COD_TIPOLOGIA='SUP_NETTA' AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND substr(rapporti_utenza.cod_contratto,1,6)<>'000000' and UNITA_CONTRATTUALE.TIPOLOGIA='AL' AND rapporti_utenza.id=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and anagrafica.id=soggetti_contrattuali.id_anagrafica and rapporti_utenza.id=soggetti_contrattuali.id_contratto and anagrafica.cod_fiscale in (select cod_fiscale from comp_nucleo_VSA where id_dichiarazione=" & Request.QueryString("ID") & ")"


                BindGrid()


            Catch ex As Exception
                Response.Write(ex.Message)

            End Try
        End If

    End Sub

    Public Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label10.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Rapporto COD. " & e.Item.Cells(0).Text & "';document.getElementById('CODICE').value='" & e.Item.Cells(0).Text & "';document.getElementById('DECORRENZA').value='" & e.Item.Cells(1).Text & "';document.getElementById('COGNOME').value='" & e.Item.Cells(7).Text & "';document.getElementById('NOME').value='" & e.Item.Cells(8).Text & "';document.getElementById('CF').value='" & e.Item.Cells(9).Text & "';document.getElementById('CODICEUNITA').value='" & e.Item.Cells(10).Text & "';")

        'End If
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Rapporto COD. " & e.Item.Cells(0).Text & "';document.getElementById('CODICE').value='" & e.Item.Cells(0).Text & "';document.getElementById('DECORRENZA').value='" & e.Item.Cells(1).Text & "';document.getElementById('COGNOME').value='" & Replace(e.Item.Cells(7).Text, "'", "\'") & "';document.getElementById('NOME').value='" & Replace(e.Item.Cells(8).Text, "'", "\'") & "';document.getElementById('CF').value='" & e.Item.Cells(9).Text & "';document.getElementById('CODICEUNITA').value='" & e.Item.Cells(10).Text & "';document.getElementById('comune').value='" & Replace(e.Item.Cells(11).Text, "'", "\'") & "';document.getElementById('cap').value='" & e.Item.Cells(12).Text & "';document.getElementById('INDIRIZZO').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('civico').value='" & Replace(e.Item.Cells(3).Text, "'", "\'") & "';document.getElementById('interno').value='" & Replace(e.Item.Cells(5).Text, "'", "\'") & "';document.getElementById('SCALA').value='" & Replace(e.Item.Cells(6).Text, "'", "\'") & "';document.getElementById('PIANO').value='" & Replace(e.Item.Cells(13).Text, "'", "\'") & "';document.getElementById('netta').value='" & Replace(e.Item.Cells(14).Text, "'", "\") & "';document.getElementById('NumVani').value='" & Replace(e.Item.Cells(15).Text, "'", "\") & "';")
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If CODICE.Value = "" Then
            Response.Write("<script>alert('Nessun Rapporto selezionato!')</script>")
        Else

            'Ccodicetrovato

            Response.Write("<script>window.opener.document.getElementById('Dom_Alloggio_ERP1_txtLocali').value='" & Replace(Replace(NumVani.Value, "&nbsp;", "0"), "'", "\'") & "';window.opener.document.getElementById('Ccodicetrovato').value='" & CODICE.Value & "';window.opener.document.getElementById('Ucodicetrovato').value='" & CODICEUNITA.Value & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtNetta').value='" & netta.Value & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_cmbAscensore').value='" & par.IfEmpty(ASCENSORE.Value, "0") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_cmbPianoUnita').value='" & Replace(PIANO.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtScala').value='" & Replace(SCALA.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtInterno').value='" & Replace(interno.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtCivico').value='" & Replace(civico.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtIndirizzo').value='" & Replace(INDIRIZZO.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtCAP').value='" & cap.Value & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtComune').value='" & Replace(comune.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value='" & CODICEUNITA.Value & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value='" & CODICE.Value & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtDecorrenza').value='" & DECORRENZA.Value & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtCognome').value='" & Replace(COGNOME.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtNome').value='" & Replace(NOME.Value, "'", "\'") & "';window.opener.document.getElementById('Dom_Alloggio_ERP1_txtCF').value='" & CF.Value & "';alert('I Dati sono stati riportati nella domanda!');</script>")
            'Response.Write("<script>window.opener.document.getElementById('form1').Dom_Dichiara_Cambi1_cu.value='" & CODICE.Value & "';alert('I Dati sono stati riportati nella domanda!');</script>")

        End If
    End Sub
End Class
