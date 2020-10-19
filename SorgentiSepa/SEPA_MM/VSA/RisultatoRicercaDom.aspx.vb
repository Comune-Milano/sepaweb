
Partial Class VSA_RisultatoRicercaDom
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
                    'Case "CONTRATTO"
                    '    sValoreST = "10"
                    'Case "ASSEGNAZIONE"
                    '    sValoreST = "9"
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

        Try

 
            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            TextBox7.Text = ex.Message
        End Try
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = True
        sStringaSql = ""

        If sValoreCG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_vsa.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_vsa.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_vsa.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " DOMANDE_BANDO_vsa.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreST <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_vsa.ID_STATO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        Else
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            sCompara = " = "

            bTrovato = True
            'sStringaSql = sStringaSql & " (DOMANDE_BANDO_vsa.ID_STATO='1' or DOMANDE_BANDO_vsa.ID_STATO='2' or DOMANDE_BANDO_vsa.ID_STATO='4' or DOMANDE_BANDO_vsa.ID_STATO='7a' or DOMANDE_BANDO_vsa.ID_STATO='8') "

        End If

        If sValoreBA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreBA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_vsa.ID_motivo_DOMANDA" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If




        sStringaSQL1 = "SELECT DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AS ID1,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME,COMP_NUCLEO_VSA.COD_FISCALE," _
            & "DOMANDE_BANDO_VSA.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
           & "DICHIARAZIONI_VSA.PG AS ""DICHIARAZIONE N°"",BANDI_VSA.DESCRIZIONE AS ""BANDO""," _
           & "DOMANDE_BANDO_VSA.ISBAR,DOMANDE_BANDO_VSA.ISBARC_R," _
           & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
           & "DOMANDE_BANDO_VSA.FL_COMPLETA AS ""PR. COMPLETA""," _
           & "DOMANDE_BANDO_VSA.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
           & " FROM DOMANDE_BANDO_VSA,TAB_STATI,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA,BANDI_VSA" _
           & " WHERE " _
           & " DOMANDE_BANDO_VSA.ID_BANDO=BANDI_VSA.ID AND DOMANDE_BANDO_VSA.ID_STATO=TAB_STATI.COD (+) " _
           & " AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID " _
           & " AND DICHIARAZIONI_VSA.ID_CAF = " & Session.Item("ID_CAF") _
           & " AND DOMANDE_BANDO_VSA.ID_STATO<>'10' AND " _
           & "DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=COMP_NUCLEO_VSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_VSA.PROGR=DOMANDE_BANDO_VSA.PROGR_COMPONENTE  "

        If Session.Item("LIVELLO") = "1" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "2" Then

            sStringaSQL1 = "SELECT DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AS ID1,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME,COMP_NUCLEO_VSA.COD_FISCALE," _
                & "DOMANDE_BANDO_VSA.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
               & "DICHIARAZIONI_VSA.PG AS ""DICHIARAZIONE N°"",BANDI_VSA.DESCRIZIONE AS ""BANDO""," _
               & "DOMANDE_BANDO_VSA.ISBAR,DOMANDE_BANDO_VSA.ISBARC_R," _
               & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
               & "DOMANDE_BANDO_VSA.FL_COMPLETA AS ""PR. COMPLETA""," _
               & "DOMANDE_BANDO_VSA.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
               & " FROM DOMANDE_BANDO_VSA,TAB_STATI,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA,BANDI_VSA" _
               & " WHERE " _
               & " DOMANDE_BANDO_VSA.ID_BANDO=BANDI_VSA.ID AND DOMANDE_BANDO_VSA.ID_STATO=TAB_STATI.COD (+) " _
               & " AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID " _
               & " " _
               & " AND DOMANDE_BANDO_VSA.ID_STATO<>'10' AND " _
               & "DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=COMP_NUCLEO_VSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_VSA.PROGR=DOMANDE_BANDO_VSA.PROGR_COMPONENTE  "

        End If

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DOMANDE_BANDO_VSA.PG ASC"

        BindGrid()
    End Function

    'Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    '    LBLID.Text = e.Item.Cells(0).Text
    '    Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
    'End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox7').value='Hai selezionato il PG: " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('ID1').value='" & e.Item.Cells(2).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox7').value='Hai selezionato il PG: " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('ID1').value='" & e.Item.Cells(2).Text & "';")

        End If


        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        'End If
        'If e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        'End If
        'If e.Item.ItemType = ListItemType.Item Or _
        '    e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TextBox7').value='Hai selezionato il PG: " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('ID1').value='" & e.Item.Cells(2).Text & "';")

        '    'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        'End If

    End Sub



    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaDomande.aspx""</script>")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            'Response.Write("<script>location.replace('../NuovaDomandaVSA/domandaNuova.aspx?ID=" & LBLID.Value & "&ID1=" & ID1.Value & "&PROGR=-1&INT=0&GLocat=1');</script>")
            Response.Write("<script>window.open('NuovaDomandaVSA/domandaNuova.aspx?ID=" & LBLID.Value & "&ID1=" & ID1.Value & "&PROGR=-1&INT=0&GLocat=1');</script>")
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
End Class
