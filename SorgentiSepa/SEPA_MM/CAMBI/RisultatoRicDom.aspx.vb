
Partial Class CAMBI_RisultatoRicDom
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreST As String
    Dim sValoreBA As String
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            sValoreCG = Request.QueryString("CG")
            sValoreNM = Request.QueryString("NM")
            sValoreCF = Request.QueryString("CF")
            sValorePG = Request.QueryString("PG")
            sValoreST = Request.QueryString("ST")
            sValoreBA = Request.QueryString("BA")
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            Select Case sValoreST
                Case "TUTTI"
                    sValoreST = ""
                Case "FORMALIZZAZIONE"
                    sValoreST = "1"
                Case "IDONEE"
                    sValoreST = "7a"
                Case "RESPINTE"
                    sValoreST = "4"
                Case "ISTRUTTORIA"
                    sValoreST = "2"
                Case "GRADUATORIA"
                    sValoreST = "8"
                Case "CONTRATTO"
                    sValoreST = "10"
                Case "ASSEGNAZIONE"
                    sValoreST = "9"
            End Select
            If sValoreBA = "-2" Then
                sValoreBA = ""
            End If

            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Private Sub Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_cambi.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_cambi.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_cambi.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_cambi.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreST <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_cambi.ID_STATO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreBA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreBA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_cambi.ID_BANDO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        sStringaSQL1 = "SELECT DOMANDE_BANDO_cambi.ID,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,COMP_NUCLEO_cambi.COD_FISCALE," _
            & "DOMANDE_BANDO_cambi.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO_cambi.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
           & "DICHIARAZIONI_cambi.PG AS ""DICHIARAZIONE N°"",BANDI_cambio.DESCRIZIONE AS ""BANDO""," _
           & "DOMANDE_BANDO_cambi.ISBAR,DOMANDE_BANDO_cambi.ISBARC_R," _
           & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
           & "DOMANDE_BANDO_cambi.FL_COMPLETA AS ""PR. COMPLETA""," _
           & "DOMANDE_BANDO_cambi.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
           & " FROM DOMANDE_BANDO_cambi,TAB_STATI,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi,BANDI_cambio" _
           & " WHERE " _
           & " DOMANDE_BANDO_cambi.ID_BANDO=BANDI_cambio.ID AND DOMANDE_BANDO_cambi.ID_STATO=TAB_STATI.COD (+) " _
           & " AND DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID " _
           & " AND DICHIARAZIONI_cambi.ID_CAF = " & Session.Item("ID_CAF") _
           & " AND DOMANDE_BANDO_cambi.ID_STATO<>'10' AND " _
           & "DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=COMP_NUCLEO_cambi.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_cambi.PROGR=DOMANDE_BANDO_cambi.PROGR_COMPONENTE  "

        If Session.Item("LIVELLO") = "1" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "2" Then

            sStringaSQL1 = "SELECT DOMANDE_BANDO_cambi.ID,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,COMP_NUCLEO_cambi.COD_FISCALE," _
                & "DOMANDE_BANDO_cambi.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO_cambi.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
               & "DICHIARAZIONI_cambi.PG AS ""DICHIARAZIONE N°"",BANDI_cambio.DESCRIZIONE AS ""BANDO""," _
               & "DOMANDE_BANDO_cambi.ISBAR,DOMANDE_BANDO_cambi.ISBARC_R," _
               & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
               & "DOMANDE_BANDO_cambi.FL_COMPLETA AS ""PR. COMPLETA""," _
               & "DOMANDE_BANDO_cambi.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
               & " FROM DOMANDE_BANDO_cambi,TAB_STATI,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi,BANDI_cambio" _
               & " WHERE " _
               & " DOMANDE_BANDO_cambi.ID_BANDO=BANDI_cambio.ID AND DOMANDE_BANDO_cambi.ID_STATO=TAB_STATI.COD (+) " _
               & " AND DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID " _
               & " " _
               & " AND DOMANDE_BANDO_cambi.ID_STATO<>'10' AND " _
               & "DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=COMP_NUCLEO_cambi.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_cambi.PROGR=DOMANDE_BANDO_cambi.PROGR_COMPONENTE  "

        End If
        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DOMANDE_BANDO_cambi.PG ASC"

        BindGrid()
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        'LBLID.Text = e.Item.Cells(0).Text
        'Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato :PG " & e.Item.Cells(1).Text & "';")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaDomande.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            Response.Write("<script>location.replace('domanda.aspx?ID=" & LBLID.Value & "&ID1=-1&PROGR=-1&INT=0');</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub
End Class
