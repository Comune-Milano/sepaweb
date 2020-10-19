
Partial Class Contratti_Report_RicercaAccertato
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            CaricaVociBolletta()
            SELEZIONA()
            txtAnno.Text = Year(Now)

        End If

    End Sub

    Private Function SELEZIONA()
        If Selezionati = "" Then
            Selezionati = 1
        Else
            Selezionati = ""
        End If
        Dim a As Integer
        Dim i As Integer = 0
        If Selezionati <> "" Then
            a = CheckVociBoll.Items.Count.ToString
            While i < a
                Me.CheckVociBoll.Items(i).Selected = True
                i = i + 1
            End While
        Else
            a = CheckVociBoll.Items.Count.ToString
            While i < a
                Me.CheckVociBoll.Items(i).Selected = False
                i = i + 1
            End While
        End If
    End Function

    Private Function SelezionaTuttoComune()

        Selezionati = 1

        Dim a As Integer
        Dim i As Integer = 0

        a = CheckVociBoll.Items.Count.ToString
        While i < a
            If Me.CheckVociBoll.Items(i).Value = 96 Or Me.CheckVociBoll.Items(i).Value = 7 Then
                Me.CheckVociBoll.Items(i).Selected = False
            Else
                Me.CheckVociBoll.Items(i).Selected = True
            End If
            i = i + 1
        End While

    End Function


    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
        End Set

    End Property

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            If IsNumeric(txtAnno.Text) = False Or Len(txtAnno.Text) <> 4 Then
                Response.Write("<script>alert('Anno non valido!');</script>")
                Exit Sub
            End If

            Dim S As String = ""
            Dim StringaSQL As String = "SELECT  T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE , TO_CHAR (SUM(BOL_BOLLETTE_VOCI.IMPORTO), '9G999G990D99') AS IMPORTO,'' AS DETTAGLI FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID  "

            Dim StringaSQLAnnullate As String = "SELECT  TO_CHAR (SUM(BOL_BOLLETTE_VOCI.IMPORTO), '9G999G990D99') AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID  "
            Dim ElencoAnnullate As String = "select bol_bollette.*,BOL_BOLLETTE_TOT.IMPORTO_TOTALE AS IMPORTO from SISCOM_MI.BOL_BOLLETTE_TOT,siscom_mi.bol_bollette where BOL_BOLLETTE.ID=BOL_BOLLETTE_TOT.ID_BOLLETTA AND  "

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim partenza As String = ""
            Dim Arrivo As String = ""
            Dim sstr As String = ""

            Select Case cmbMensilita.SelectedItem.Value
                Case "0"
                    partenza = "0101"
                    Arrivo = "0229"
                Case "1"
                    partenza = "0301"
                    Arrivo = "0430"
                Case "2"
                    partenza = "0501"
                    Arrivo = "0631"
                Case "3"
                    partenza = "0701"
                    Arrivo = "0831"
                Case "4"
                    partenza = "0901"
                    Arrivo = "1031"
                Case "5"
                    partenza = "1101"
                    Arrivo = "1231"
            End Select

            partenza = txtAnno.Text & partenza
            Arrivo = txtAnno.Text & Arrivo


            sstr = " "
            par.cmd.CommandText = "SELECT distinct rif_file FROM SISCOM_MI.bol_bollette where  n_rata<>99 and n_rata<>99999 and n_rata<>999 and SUBSTR(RIF_FILE,1,3)='BO_' and riferimento_da>='" & partenza & "' and riferimento_a<='" & Arrivo & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                sstr = sstr & " rif_file='" & par.IfNull(myReader("rif_file"), "") & "' OR "
            End While
            myReader.Close()

            If sstr = " " Then
                sstr = sstr & " (rif_file='xxx') "
            Else
                sstr = "( " & Mid(sstr, 1, Len(sstr) - 3) & ") "
            End If


            StringaSQL = StringaSQL & " AND  " & sstr
            StringaSQLAnnullate = StringaSQLAnnullate & " AND  " & sstr
            ElencoAnnullate = ElencoAnnullate & " AND  " & sstr


            '*********FINE SEZIONE INERENTE ALLE DATE

            If Selezionati = "1" Then
                StringaSQL = StringaSQL & " AND ("
                StringaSQLAnnullate = StringaSQLAnnullate & " AND ("
                ElencoAnnullate = ElencoAnnullate & " AND BOL_BOLLETTE.ID IN ( SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE "

                Dim i As Integer = 0
                Dim primo As Boolean = False
                For Each o As Object In CheckVociBoll.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If item.Selected Then
                        If primo = False Then
                            S = " ID_VOCE =" & item.Value
                            primo = True
                        Else
                            S = S & " OR ID_VOCE =" & item.Value
                        End If

                    End If
                Next
                StringaSQL = StringaSQL & S & ")"
                StringaSQLAnnullate = StringaSQLAnnullate & S & ")"
                ElencoAnnullate = ElencoAnnullate & S & ")  ORDER BY BOL_BOLLETTE.INTESTATARIO ASC"
            Else

                StringaSQL = StringaSQL & " AND ("
                StringaSQLAnnullate = StringaSQLAnnullate & " AND ("
                ElencoAnnullate = ElencoAnnullate & " AND BOL_BOLLETTE.ID IN ( SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE "

                Dim i As Integer = 0
                Dim primo As Boolean = False

                For Each o As Object In CheckVociBoll.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If primo = False Then
                        S = " ID_VOCE <>" & item.Value
                        primo = True
                    Else
                        S = S & " and ID_VOCE <>" & item.Value
                    End If

                Next
                StringaSQL = StringaSQL & S & ")"
                StringaSQLAnnullate = StringaSQLAnnullate & S & ")"
                ElencoAnnullate = ElencoAnnullate & S & ") ORDER BY BOL_BOLLETTE.INTESTATARIO ASC"


            End If

            If Selezionati <> "1" Then
                Response.Write("<script>alert('Selezionare almeno una voce!');</script>")
                Exit Sub
            End If

            Session.Add("REPORT", StringaSQL)
            Session.Add("REPORT1", StringaSQLAnnullate)
            Session.Add("REPORT2", ElencoAnnullate)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>window.open('StampaD_SingoleVoci.aspx?USD=false&G=1&O=0&DAL1=" & partenza & "&AL1=" & Arrivo & "&DAL=&AL=&TIPO=Bollettazione');</script>")


        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message

        End Try

    End Sub
    Private Sub CaricaVociBolletta()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Me.CheckVociBoll.Items.Clear()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA  WHERE id<10000  ORDER BY DESCRIZIONE ASC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                CheckVociBoll.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
            End While
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub ChComune_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChComune.CheckedChanged
        If ChComune.Checked = True Then
            SelezionaTuttoComune()
            CheckVociBoll.Enabled = False
            btnSelezionaTutto.Enabled = False
        Else
            CheckVociBoll.Enabled = True
            btnSelezionaTutto.Enabled = True
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Contabilita/pagina_home.aspx""</script>")
    End Sub

    Protected Sub CheckVociBoll_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckVociBoll.SelectedIndexChanged
        Selezionati = 1
    End Sub

    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
        SELEZIONA()
    End Sub
End Class
