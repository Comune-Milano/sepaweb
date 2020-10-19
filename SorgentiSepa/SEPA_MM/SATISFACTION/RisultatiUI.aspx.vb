
Partial Class VSA_RisultatiUI
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String = ""
    Dim vEdificio As String
    Dim vIndirizzo As String
    Dim vCivico As String
    Dim vTipo As String
    Dim vComplesso As String
    Dim vInterno As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../CENSIMENTO/IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then

            Response.Flush()

            vEdificio = Request.QueryString("E")
            vIndirizzo = Request.QueryString("I")
            vCivico = Request.QueryString("CIV")
            vTipo = Request.QueryString("TIPOL")
            vComplesso = Request.QueryString("COMP")
            vInterno = Request.QueryString("INT")
            'LBLID.Text = "-1"
            LblEdificio.Text = vEdificio
            If vEdificio <> "-1" Then
                If Request.QueryString("T") = "0" Then
                    CercaSelettiva()

                Else
                    cerca()

                End If

            End If
        End If
    End Sub

    Private Sub cerca()

        Dim bTrovato As Boolean
        Dim sValore As String
        Dim condizione As String = ""
        Try
            bTrovato = False
            sStringaSql = ""
            sStringaSql = "SELECT DISTINCT ROWNUM, TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO, COMUNI_NAZIONI.NOME AS COMUNE,INDIRIZZI.CAP,INDIRIZZI.CIVICO,INDIRIZZI.DESCRIZIONE AS INDIRIZZO,EDIFICI.DENOMINAZIONE, SISCOM_MI.UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE ,UNITA_IMMOBILIARI.INTERNO,  (SCALE_EDIFICI.DESCRIZIONE) AS SCALA ,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB,dimensioni.valore as netta FROM siscom_mi.dimensioni, SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.identificativi_catastali where unita_immobiliari.id=dimensioni.id_unita_immobiliare (+) and dimensioni.cod_tipologia='SUP_NETTA' AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO =TIPO_LIVELLO_PIANO.COD (+) AND UNITA_IMMOBILIARI.ID_SCALA= SCALE_EDIFICI.ID(+) AND TIPOLOGIA_UNITA_IMMOBILIARI.COD= UNITA_IMMOBILIARI.COD_TIPOLOGIA and SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO and SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID(+) and SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+)AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL' and  SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO<>1 "

            If vEdificio <> "" And vEdificio <> "-1" Then
                sValore = vEdificio
                'sValore = Mid(sValore, 1, InStr(sValore, "-") - 1)

                bTrovato = True
                condizione = "AND UNITA_IMMOBILIARI.ID_EDIFICIO = '" & par.PulisciStrSql(sValore) & "'"
            End If

            If vComplesso <> "" And vComplesso <> "-1" Then
                sValore = vComplesso
                bTrovato = True
                condizione = condizione & "AND EDIFICI.ID_COMPLESSO =" & sValore
            End If

            If par.IfEmpty(vIndirizzo, "Null") <> "Null" Then

                sValore = vIndirizzo

                condizione = condizione & "AND EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & (sValore) & "' "

                If par.IfEmpty(vCivico, "Null") <> "Null" Then
                    sValore = vCivico
                    condizione = condizione & "AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
                End If
                condizione = condizione & ")"


            End If

            If vTipo <> "" And vTipo <> "-1" Then
                sValore = vTipo
                condizione = condizione & "AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='" & sValore & "'"
            End If

            If par.IfEmpty(vInterno, "Null") <> "Null" Then
                sValore = vInterno
                condizione = condizione & " AND UNITA_IMMOBILIARI.INTERNO ='" & par.PulisciStrSql(sValore) & "' "

            End If

            If condizione <> "" Then
                sStringaSql = sStringaSql & condizione

            End If
            sStringaSql = sStringaSql & " ORDER BY ROWNUM ASC"
            QUERY = sStringaSql
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub CercaSelettiva()

        Try
            QUERY = Session.Item("PED")
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

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
        da.Fill(ds, "UNITA_COMUNI, INDIRIZZI,COMUNI_NAZIONI")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaUI.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")

            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità COD. " & Replace(e.Item.Cells(0).Text, "'", "\'") & "';document.getElementById('CODICE').value='" & e.Item.Cells(0).Text & "';document.getElementById('COMUNE').value='" & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('CAP').value='" & e.Item.Cells(4).Text & "';document.getElementById('INDIRIZZO').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('CIVICO').value='" & Replace(e.Item.Cells(3).Text, "'", "\'") & "';document.getElementById('INTERNO').value='" & Replace(e.Item.Cells(5).Text, "'", "\'") & "';document.getElementById('SCALA').value='" & Replace(e.Item.Cells(6).Text, "'", "\'") & "';document.getElementById('PIANO').value='" & Replace(e.Item.Cells(7).Text, "'", "\'") & "';document.getElementById('netta').value='" & Replace(e.Item.Cells(8).Text, "'", "\'") & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità COD. " & e.Item.Cells(0).Text & "';document.getElementById('CODICE').value='" & e.Item.Cells(0).Text & "';document.getElementById('COMUNE').value='" & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('CAP').value='" & e.Item.Cells(4).Text & "';document.getElementById('INDIRIZZO').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('CIVICO').value='" & Replace(e.Item.Cells(3).Text, "'", "\'") & "';document.getElementById('INTERNO').value='" & Replace(e.Item.Cells(5).Text, "'", "\'") & "';document.getElementById('SCALA').value='" & Replace(e.Item.Cells(6).Text, "'", "\'") & "';document.getElementById('PIANO').value='" & Replace(e.Item.Cells(7).Text, "'", "\'") & "';document.getElementById('netta').value='" & Replace(e.Item.Cells(8).Text, "'", "\'") & "';")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If CODICE.Value = "" Then
            Response.Write("<script>alert('Nessuna Unita selezionata!');</script>")
        Else
            'Response.Redirect("InserimentoUniImmob.aspx?C=RisultatiUI&COMPLESSO=" & Request.QueryString("COMP") & "&EDIFICIO=" & Request.QueryString("E") & "&INDIRIZZO=" & Request.QueryString("I") & "&CIVICO=" & Request.QueryString("CIV") & "&ID=" & txtid.Text & "&TIPOL=" & Request.QueryString("TIPOL") & "&T=" & Request.QueryString("T"))
            Session.Add("COD_UNITA_VSA", txtid.Text)

            Response.Write("<script>window.opener.document.getElementById('form1').txt_codui.value='" & CODICE.Value & "';self.close();</script>")

            'Response.Write("<script>window.opener.document.getElementById('form1').Dom_Dichiara_Cambi1_cu.value='" & CODICE.Value & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtCodiceUnita.value='" & CODICE.Value & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtComune.value='" & Replace(comune.Value, "'", "\'") & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtCAP.value='" & cap.Value & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtIndirizzo.value='" & Replace(INDIRIZZO.Value, "'", "\'") & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtCivico.value='" & Replace(CIVICO.Value, "'", "\'") & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtInterno.value='" & Replace(INTERNO.Value, "'", "\'") & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtScala.value='" & Replace(SCALA.Value, "'", "\'") & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtPiano.value='" & Replace(CIVICO.Value, "'", "\'") & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_cmbAscensore.value='" & par.IfEmpty(ASCENSORE.Value, "0") & "';window.opener.document.getElementById('form1').Dom_Alloggio_ERP1_txtNetta.value='" & netta.Value & "';alert('I Dati sono stati riportati nella domanda!');</script>")
        End If
    End Sub
End Class
