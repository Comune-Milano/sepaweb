
Partial Class VSA_RisultatoRicercaD
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreST As String
    Dim sValoreBA As String
    Dim sValoreTT As String
    Dim sStringaSql As String
    Dim scriptblock As String
    Dim sValoreInval As String

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
            sValoreTT = Request.QueryString("TT")
            sValoreInval = Request.QueryString("INVAL")
            'sValoreBA = Request.QueryString("BA")
            If sValoreST = -1 Then sValoreST = ""
            If sValoreTT = "-1" Then sValoreTT = ""
            sValoreBA = ""
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Private Sub BindGrid()

        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "DICHIARAZIONI_VSA,COMP_NUCLEO_VSA")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label7.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_VSA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_VSA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_VSA.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreST <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.ID_STATO " & sCompara & " " & par.PulisciStrSql(sValore) & " "
        End If

        If sValoreTT <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreTT
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.TIPO= " & par.PulisciStrSql(sValore) & " "
        End If


        If sValoreBA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreBA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.ID_BANDO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If





        If (sValoreInval <> "" And sValoreInval <> "0") Then
            If sValoreInval = "SI" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " id_dichiarazione IN ( SELECT id_dichiarazione FROM comp_nucleo_vsa, dichiarazioni_vsa WHERE dichiarazioni_vsa.ID = comp_nucleo_vsa.id_dichiarazione AND natura_inval = 'Motoria con carrozzella') "
            Else
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " id_dichiarazione NOT IN ( SELECT id_dichiarazione FROM comp_nucleo_vsa, dichiarazioni_vsa WHERE dichiarazioni_vsa.ID = comp_nucleo_vsa.id_dichiarazione AND natura_inval = 'Motoria con carrozzella') "
            End If
        End If





        sStringaSQL1 = "SELECT DICHIARAZIONI_VSA.ID,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME," _
                     & "COMP_NUCLEO_VSA.COD_FISCALE ," _
                     & "DICHIARAZIONI_VSA.PG ," _
                     & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY')" _
                     & " AS  ""DATA_PG""," _
                    & "T_STATI_DICHIARAZIONE.DESCRIZIONE AS ""STATO"" " _
                    & " FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,T_STATI_DICHIARAZIONE WHERE " _
                    & "  DICHIARAZIONI_VSA.ID_CAF = " & Session.Item("ID_CAF") _
                    & "AND DICHIARAZIONI_VSA.PG IS NOT NULL AND DICHIARAZIONI_VSA.ID=COMP_NUCLEO_VSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_VSA.PROGR=0" _
                    & " AND DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD (+)"

        If Session.Item("LIVELLO") = "1" Or Session.Item("ID_CAF") = "6" Then

            sStringaSQL1 = "SELECT DICHIARAZIONI_VSA.ID,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME," _
                        & "COMP_NUCLEO_VSA.COD_FISCALE ," _
                        & "DICHIARAZIONI_VSA.PG ," _
                        & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY')" _
                        & " AS  ""DATA_PG""," _
                        & "T_STATI_DICHIARAZIONE.DESCRIZIONE AS ""STATO"" " _
                        & " FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,T_STATI_DICHIARAZIONE WHERE " _
                        & " DICHIARAZIONI_VSA.PG IS NOT NULL AND DICHIARAZIONI_VSA.ID=COMP_NUCLEO_VSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_VSA.PROGR=0" _
                        & " AND DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD (+)"

        End If

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DICHIARAZIONI_VSA.PG ASC"

        BindGrid()
    End Function

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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaDichiarazioni.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Dichiarazione selezionata!')</script>")
        Else
            'scriptblock = "<script language='javascript' type='text/javascript'>" _
            '& "location.replace('max.aspx?ID=" & LBLID.Value & "&INT=0&GLocat=1');" _
            '& "</script>"
            'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
            '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
            'End If
            Response.Write("<script>window.open('NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & LBLID.Value & "&INT=0&GLocat=1','DichNew')</script>")
        End If
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';")
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


End Class
