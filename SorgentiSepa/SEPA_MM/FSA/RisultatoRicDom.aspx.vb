
Partial Class FSA_RisultatoRicDom
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreST As String
    Dim sValoreBA As String
    Dim sValoreLI As String
    Dim sValoreMA As String
    Dim sValoreOP As String
    Dim sValoreRE As String
    Dim sValoreDAL As String
    Dim sValoreAL As String
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
            sValoreLI = Request.QueryString("LI")
            sValoreMA = Request.QueryString("MA")
            sValoreOP = Request.QueryString("OP")
            sValoreRE = Request.QueryString("RE")
            sValoreDAL = Request.QueryString("DAL")
            sValoreAL = Request.QueryString("AL")

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

            If sValoreOP = "-1" Then
                sValoreOP = ""
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
        da.Fill(ds, "DOMANDE_BANDO_FSA,COMP_NUCLEO_FSA")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_FSA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_FSA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_FSA.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " DOMANDE_BANDO_FSA.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreST <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_FSA.ID_STATO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreBA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreBA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_FSA.ID_BANDO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreLI <> "" And sValoreLI <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreLI
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_FSA.FL_DA_LIQUIDARE" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreMA <> "" And sValoreMA <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreMA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_FSA.FL_MANDATO_EFF" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreOP <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreOP
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_FSA.ID_OPERATORE_CARICO=" & par.PulisciStrSql(sValore) & " "
        End If

        Select Case sValoreRE
            Case "1"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito1='0' "
            Case "2"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito2='0' "
            Case "3"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito3='0' "
            Case "4"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito4='0' "
            Case "5"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito5='0' "

            Case "7"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito7='0' "
            Case "8"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito8='0' "
            Case "9"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito9='0' "
            Case "10"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito10='0' "
            Case "11"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito11='0' "
            Case "12"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito12='0' "
            Case "13"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito13='0' "
            Case "14"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.requisito14='0' "
            Case "15"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.IDONEO_PRESUNTO='6' "

            Case "16"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.IDONEO_PRESUNTO='8' "

            Case "17"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.IDONEO_PRESUNTO='4' "


            Case "18"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.IDONEO_PRESUNTO='2' "
            Case "19"
                sStringaSql = sStringaSql & " and DOMANDE_BANDO_FSA.IDONEO_PRESUNTO='1' "




        End Select

        If sValoreDAL <> "" And sValoreAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_FSA.ID IN (SELECT ID_DOMANDA FROM EVENTI_BANDI_FSA WHERE COD_EVENTO='F57' AND DATA_ORA>='" & par.AggiustaData(sValoreDAL) & "000000' AND DATA_ORA<='" & par.AggiustaData(sValoreAL) & "595959') "
        End If

        If Session.Item("LIVELLO") <> "1" And Session.Item("ID_CAF") <> "6" Then
            sStringaSQL1 = "SELECT DOMANDE_BANDO_FSA.ID,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME,COMP_NUCLEO_FSA.COD_FISCALE," _
                & "DOMANDE_BANDO_FSA.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO_FSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
               & "DICHIARAZIONI_FSA.PG AS ""DICHIARAZIONE N°"",BANDI_FSA.DESCRIZIONE AS ""BANDO""," _
               & "" _
               & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
               & "DOMANDE_BANDO_FSA.FL_COMPLETA AS ""PR. COMPLETA""," _
               & "DOMANDE_BANDO_FSA.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
               & " FROM DOMANDE_BANDO_FSA,TAB_STATI,COMP_NUCLEO_FSA,DICHIARAZIONI_FSA,BANDI_FSA" _
               & " WHERE " _
               & " DOMANDE_BANDO_FSA.ID_BANDO=BANDI_FSA.ID AND DOMANDE_BANDO_FSA.ID_STATO=TAB_STATI.COD (+) " _
               & " AND DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=DICHIARAZIONI_FSA.ID " _
               & " AND DICHIARAZIONI_FSA.ID_CAF = " & Session.Item("ID_CAF") _
               & " AND DOMANDE_BANDO_FSA.ID_STATO<>'10' AND " _
               & "DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=COMP_NUCLEO_FSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_FSA.PROGR=DOMANDE_BANDO_FSA.PROGR_COMPONENTE  "
        Else
            sStringaSQL1 = "SELECT DOMANDE_BANDO_FSA.ID,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME,COMP_NUCLEO_FSA.COD_FISCALE," _
                        & "DOMANDE_BANDO_FSA.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO_FSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
                        & "DICHIARAZIONI_FSA.PG AS ""DICHIARAZIONE N°"",BANDI_FSA.DESCRIZIONE AS ""BANDO""," _
                        & "" _
                        & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
                        & "DOMANDE_BANDO_FSA.FL_COMPLETA AS ""PR. COMPLETA""," _
                        & "DOMANDE_BANDO_FSA.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
                        & " FROM DOMANDE_BANDO_FSA,TAB_STATI,COMP_NUCLEO_FSA,DICHIARAZIONI_FSA,BANDI_FSA" _
                        & " WHERE " _
                        & " DOMANDE_BANDO_FSA.ID_BANDO=BANDI_FSA.ID AND DOMANDE_BANDO_FSA.ID_STATO=TAB_STATI.COD (+) " _
                        & " AND DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=DICHIARAZIONI_FSA.ID " _
                        & " " _
                        & " AND DOMANDE_BANDO_FSA.ID_STATO<>'10' AND " _
                        & "DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=COMP_NUCLEO_FSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_FSA.PROGR=DOMANDE_BANDO_FSA.PROGR_COMPONENTE  "


        End If


        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DOMANDE_BANDO_FSA.PG ASC"

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
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato: PG " & e.Item.Cells(1).Text & "';")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "'")
            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
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
