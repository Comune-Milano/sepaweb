Namespace CM

Partial Class RisultatoRicDom
        Inherits PageSetIdMode
        Dim par As New [Global]()
        Dim sValoreCG As String
        Dim sValoreNM As String
        Dim sValoreCF As String
        Dim sValorePG As String
        Dim sValoreST As String
        Dim sValoreBA As String
        Dim sStringaSql As String


#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()
    End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "</div>"

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
                End Select
                If sValoreBA = "-2" Then
                    sValoreBA = ""
                End If
                'Select Case sValoreBA
                '    Case "TUTTI"
                '        sValoreBA = ""
                '    Case "BANDO GENERALE"
                '        sValoreBA = "1"
                '    Case "BANDO I° SEMESTRE 2007"
                '        sValoreBA = "3"
                '    Case "BANDO II° SEMESTRE 2007"
                '        sValoreBA = "4"
                'End Select

                LBLID.Value = "-1"
                Cerca()
                'Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                'Response.Flush()
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
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
                    SCOMPARA = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    SCOMPARA = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " COMP_NUCLEO.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
                sStringaSql = sStringaSql & " COMP_NUCLEO.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
                sStringaSql = sStringaSql & " COMP_NUCLEO.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
                sStringaSql = sStringaSql & " DOMANDE_BANDO.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreST <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreST
                sCompara = " = "

                bTrovato = True
                sStringaSql = sStringaSql & " DOMANDE_BANDO.ID_STATO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreBA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreBA
                sCompara = " = "

                bTrovato = True
                sStringaSql = sStringaSql & " DOMANDE_BANDO.ID_BANDO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            sStringaSQL1 = "SELECT DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE," _
                & "DOMANDE_BANDO.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
               & "DICHIARAZIONI.PG AS ""DICHIARAZIONE N°"",BANDI.DESCRIZIONE AS ""BANDO""," _
               & "DOMANDE_BANDO.ISBAR,DOMANDE_BANDO.ISBARC_R," _
               & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
               & "DOMANDE_BANDO.FL_COMPLETA AS ""PR. COMPLETA""," _
               & "DOMANDE_BANDO.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
               & " FROM DOMANDE_BANDO,TAB_STATI,COMP_NUCLEO,DICHIARAZIONI,BANDI" _
               & " WHERE " _
               & " DOMANDE_BANDO.ID_BANDO=BANDI.ID AND DOMANDE_BANDO.ID_STATO=TAB_STATI.COD (+) " _
               & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
               & " AND DICHIARAZIONI.ID_CAF = " & Session.Item("ID_CAF") _
               & " AND DOMANDE_BANDO.ID_STATO<>'10' AND " _
               & "DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE  "


            If Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "61" Or Session.Item("LIVELLO") = "1" Then
                sStringaSQL1 = "SELECT DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE," _
                    & "DOMANDE_BANDO.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
                   & "DICHIARAZIONI.PG AS ""DICHIARAZIONE N°"",BANDI.DESCRIZIONE AS ""BANDO""," _
                   & "DOMANDE_BANDO.ISBAR,DOMANDE_BANDO.ISBARC_R," _
                   & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
                   & "DOMANDE_BANDO.FL_COMPLETA AS ""PR. COMPLETA""," _
                   & "DOMANDE_BANDO.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
                   & " FROM DOMANDE_BANDO,TAB_STATI,COMP_NUCLEO,DICHIARAZIONI,BANDI" _
                   & " WHERE " _
                   & " DOMANDE_BANDO.ID_BANDO=BANDI.ID AND DOMANDE_BANDO.ID_STATO=TAB_STATI.COD (+) " _
                   & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
                   & " " _
                   & " AND DOMANDE_BANDO.ID_STATO<>'10' AND " _
                   & "DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE  "
            End If

            If sStringaSql <> "" Then
                sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
            End If
            sStringaSQL1 = sStringaSQL1 & " ORDER BY DOMANDE_BANDO.PG ASC"
            'par.OracleConn.Open()
            'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            'Label3.Text = "0"
            'Do While myReader.Read()
            '    Label3.Text = CInt(Label3.Text) + 1
            'Loop
            'Label3.Text = Label3.Text
            'cmd.Dispose()
            'myReader.Close()
            'par.OracleConn.Close()
            BindGrid()
        End Function

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


        Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

        End Sub
    End Class

End Namespace