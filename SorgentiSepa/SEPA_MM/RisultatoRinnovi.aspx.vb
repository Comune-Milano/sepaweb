
Partial Class RisultatoRinnovi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreST As String
    Dim sValoreBA As String
    Dim sStringaSql As String


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaRinnovi.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            Response.Write("<script>location.replace('Rinnovo.aspx?ID=" & LBLID.Value & "');</script>")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            sValoreCG = Request.QueryString("CG").ToUpper
            sValoreNM = Request.QueryString("NM").ToUpper
            sValoreCF = Request.QueryString("CF").ToUpper
            sValorePG = Request.QueryString("PG")
            sValoreBA = Request.QueryString("BA")
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            If sValoreBA = "-2" Then sValoreBA = ""
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
        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String
        Dim ULTIMO_BAND0 As Long

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


        sStringaSQL1 = "SELECT MAX(ID) FROM BANDI WHERE STATO=1"

        par.OracleConn.Open()
        par.SettaCommand(par)
        par.cmd.CommandText = sStringaSQL1
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            ULTIMO_BAND0 = myReader1(0)
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=115"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            If par.IfNull(myReader1(0), "0") = "1" Then
                par.cmd.CommandText = "select id from bandi order by id desc"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    myReader2.Read()
                    myReader2.Read()
                    myReader2.Read()
                    ULTIMO_BAND0 = myReader2(0)
                End If
                myReader2.Close()
            End If
        End If
        myReader1.Close()

        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        If sValoreBA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreBA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO.ID_BANDO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        Else
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreBA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " (DOMANDE_BANDO.ID_BANDO<>-1 AND domande_bando.id_bando<=" & ULTIMO_BAND0 & ") "

        End If


        If Session.Item("ID_CAF") = 6 Or Session.Item("ID_CAF") = 2 Then
            sStringaSQL1 = "SELECT DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE," _
                & "DOMANDE_BANDO.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO.DATA_PG,'yyyyMMdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
               & "DICHIARAZIONI.PG AS ""DICHIARAZIONE N°"",BANDI.DESCRIZIONE AS ""BANDO""," _
               & "DOMANDE_BANDO.ISBAR,DOMANDE_BANDO.ISBARC_R," _
               & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
               & "DOMANDE_BANDO.FL_COMPLETA AS ""PR. COMPLETA""," _
               & "DOMANDE_BANDO.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
               & " FROM DOMANDE_BANDO,TAB_STATI,COMP_NUCLEO,DICHIARAZIONI,BANDI" _
               & " WHERE " _
               & " DOMANDE_BANDO.ID_BANDO=BANDI.ID AND DOMANDE_BANDO.ID_STATO=TAB_STATI.COD (+) " _
               & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
               & " AND (DOMANDE_BANDO.ID_STATO='4' or DOMANDE_BANDO.ID_STATO='8' OR (DOMANDE_BANDO.ID_STATO='2' AND (DOMANDE_BANDO.ID_TIPO_CONTENZIOSO=138 OR DOMANDE_BANDO.ID_TIPO_CONTENZIOSO=140 OR DOMANDE_BANDO.ID_TIPO_CONTENZIOSO=153) ))  AND (DOMANDE_BANDO.FL_INIZIO_REQ='0' OR DOMANDE_BANDO.FL_INIZIO_REQ='2' OR DOMANDE_BANDO.FL_INIZIO_REQ IS NULL) " _
               & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE and domande_bando.id not in (select id_domanda from domande_esclusioni where id_tipo_esclusione=11 or id_tipo_esclusione=0 or id_tipo_esclusione=4 or id_tipo_esclusione=5 or id_tipo_esclusione=1 or id_tipo_esclusione=2) "
        Else
            sStringaSQL1 = "SELECT DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE," _
    & "DOMANDE_BANDO.PG AS ""PG"",TO_CHAR(TO_DATE(DOMANDE_BANDO.DATA_PG,'yyyyMMdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
   & "DICHIARAZIONI.PG AS ""DICHIARAZIONE N°"",BANDI.DESCRIZIONE AS ""BANDO""," _
   & "DOMANDE_BANDO.ISBAR,DOMANDE_BANDO.ISBARC_R," _
   & "TAB_STATI.DESCRIZIONE AS ""STATO""," _
   & "DOMANDE_BANDO.FL_COMPLETA AS ""PR. COMPLETA""," _
   & "DOMANDE_BANDO.FL_PRATICA_CHIUSA AS ""PR. CHIUSA""" _
   & " FROM DOMANDE_BANDO,TAB_STATI,COMP_NUCLEO,DICHIARAZIONI,BANDI" _
   & " WHERE " _
   & " DOMANDE_BANDO.ID_BANDO=BANDI.ID AND DOMANDE_BANDO.ID_STATO=TAB_STATI.COD (+) " _
   & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
   & " AND (DOMANDE_BANDO.ID_STATO='4' or DOMANDE_BANDO.ID_STATO='8' OR (DOMANDE_BANDO.ID_STATO='2' AND (DOMANDE_BANDO.ID_TIPO_CONTENZIOSO=138 OR DOMANDE_BANDO.ID_TIPO_CONTENZIOSO=140 OR DOMANDE_BANDO.ID_TIPO_CONTENZIOSO=153) ))  AND (DOMANDE_BANDO.FL_INIZIO_REQ='0' OR DOMANDE_BANDO.FL_INIZIO_REQ='2' OR DOMANDE_BANDO.FL_INIZIO_REQ IS NULL) " _
   & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE and domande_bando.id not in (select id_domanda from domande_esclusioni where id_tipo_esclusione=11 or id_tipo_esclusione=0 or id_tipo_esclusione=4 or id_tipo_esclusione=5 or id_tipo_esclusione=1 or id_tipo_esclusione=2) " _
   '& " and domande_bando.id not in (select id_domanda from eventi_bandi where cod_evento='F39') "

        End If
        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DOMANDE_BANDO.PG ASC"


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
