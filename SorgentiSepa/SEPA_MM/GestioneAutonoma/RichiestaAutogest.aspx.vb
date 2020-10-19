
Partial Class GestioneAutonoma_RichiestaAutogest
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public dtEdifici As Data.DataTable

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Response.Write("<script>window.open('Modelli/ModelloA.aspx?EDSELEZIONATI=" & Me.vSelezionati & "', '');</script>")
    End Sub
    Public Property vSelezionati() As String
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
    Private Function CalcolaMorosità() As Boolean

        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim bollettato As String
            Dim pagato As String


            If vSelezionati <> "" Then
                par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO)AS BOLLETTATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE " _
                    & " WHERE BOL_BOLLETTE.ID_EDIFICIO IN  (" & vSelezionati & ") AND (SISCOM_MI.GETSTATOCONTRATTO(BOL_BOLLETTE.ID_CONTRATTO) = 'IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(ID_CONTRATTO)='IN CORSO (S.T.)')  AND BOL_BOLLETTE.FL_ANNULLATA = 0 AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA"
            End If


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                bollettato = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
            End If
            myReader1.Close()
            If vSelezionati <> "" Then
                par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO)AS BOLLETTATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE " _
                    & " WHERE BOL_BOLLETTE.ID_EDIFICIO IN  (" & vSelezionati & ") AND (SISCOM_MI.GETSTATOCONTRATTO(BOL_BOLLETTE.ID_CONTRATTO) = 'IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(ID_CONTRATTO)='IN CORSO (S.T.)')  AND BOL_BOLLETTE.FL_ANNULLATA = 0 AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA"
            End If


            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                pagato = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
            End If
            myReader1.Close()

            If par.IfEmpty(bollettato, 0) <> 0 Then
                Me.lblPercentuale.Text = Format(((bollettato - pagato) * 100) / bollettato, "##,##0.00") & "%"
            Else
                Me.lblPercentuale.Text = "0,00%"
            End If

            ' '' ''PERCENTUALE(ABUSIVISMO)
            If vSelezionati <> "" Then
                par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID IN  (" & vSelezionati & ")"
            End If
            myReader1 = par.cmd.ExecuteReader()
            Dim TotUnitaImmob As Double
            If myReader1.Read Then
                TotUnitaImmob = myReader1(0)
            End If
            myReader1.Close()
            If vSelezionati <> "" Then
                par.cmd.CommandText = "SELECT COUNT(ID_UNITA) AS ABUSIVI FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI, SISCOM_MI.RAPPORTI_UTENZA WHERE EDIFICI.ID= UNITA_CONTRATTUALE.ID_EDIFICIO AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = 'NONE' AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID IN  (" & vSelezionati & ")"
            End If
            myReader1 = par.cmd.ExecuteReader()
            Dim OccAbus As Double
            If myReader1.Read Then
                OccAbus = myReader1(0)
            End If

            myReader1.Close()

            Dim PercOccAbus As Double
            If TotUnitaImmob > 0 Then
                PercOccAbus = (OccAbus * 100) / TotUnitaImmob
            Else
                PercOccAbus = 0
            End If
            Me.lblPercOccAbu.Text = Format(PercOccAbus, "##,##0.00") & "%"

            If Me.lblPercentuale.Text.Replace("%", "") > 15 OrElse PercOccAbus > 10 Then
                Response.Write("<script>alert('ATTENZIONE!La morosità dell\'immobile non consente la costituzione dell\'Autogestione');</script>")
                CalcolaMorosità = True
                Me.btnStampa.Visible = False
                Me.lblPercentuale.ForeColor = Drawing.Color.Red
                Me.lblPercOccAbu.ForeColor = Drawing.Color.Red
                Me.lblPercOccAbu.Text = PercOccAbus & "%"
            Else
                Me.btnStampa.Visible = True
                Me.lblPercentuale.ForeColor = Drawing.Color.Black
                Me.lblPercOccAbu.ForeColor = Drawing.Color.Black
                Me.txtvisibility.Value = 2
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try

    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim gest As Integer = 0
            Me.cmbComplessi.Items.Clear()
            cmbComplessi.Items.Add(New ListItem(" ", -1))
            par.cmd.CommandText = "SELECT distinct COMPLESSI_IMMOBILIARI.id,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,COMPLESSI_IMMOBILIARI.DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbComplessi.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ListaSelezionati", New ListItemCollection)

            CaricaEdifici()

        End If

    End Sub
    Private Sub CaricaEdifici()
        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Me.cmbComplessi.SelectedValue = "-1" Then
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and COMPLESSI_IMMOBILIARI.ID <> 1 order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione, EDIFICI.ID_COMPLESSO FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and COMPLESSI_IMMOBILIARI.ID = " & Me.cmbComplessi.SelectedValue.ToString & " order by denominazione asc"

            End If
            Me.ListEdifici.Items.Clear()
            dtEdifici = New Data.DataTable
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dtEdifici)
            For Each row As Data.DataRow In dtEdifici.Rows
                ListEdifici.Items.Add(New ListItem(par.IfNull(row.Item("denominazione"), " ") & "- -" & "cod." & par.IfNull(row.Item("COD_EDIFICIO"), " "), par.IfNull(row.Item("id"), -1)))
            Next


            For Each i As ListItem In DirectCast(Session.Item("ListaSelezionati"), ListItemCollection)
                If Not IsNothing(ListEdifici.Items.FindByValue(i.Value)) Then
                    ListEdifici.Items.FindByValue(i.Value).Selected = True
                End If
            Next

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaEdifici - " & ex.Message
        End Try
    End Sub

    Protected Sub btnVerifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVerifica.Click

        If Me.vSelezionati <> "" Then
            CalcolaMorosità()
        Else
            Response.Write("<script>alert('ATTENZIONE!Selezionare almeno un immobile per effetuare la Verifica!');</script>")

        End If


    End Sub

    Protected Sub imgAggiornaEdifici_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAggiornaEdifici.Click
        addSelEdifici()
        Me.txtvisibility.Value = 0
    End Sub
    Private Sub addSelEdifici()
        Try
            Dim COLORE As String = "#E6E6E6"
            lblEdifici.Text = ""
            Dim testoTabella As String
            'Me.cmbEdifScelti.Items.Clear()
            Dim ListaSelezionati As New ListItemCollection

            If Selezionati(ListEdifici) = True Then
                testoTabella = ""
                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
                Dim I As Integer
                For I = 0 To Me.ListEdifici.Items.Count() - 1
                    If Me.ListEdifici.Items(I).Selected = True Then
                        testoTabella = testoTabella _
                            & "<tr bgcolor = '" & COLORE & "'>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & ListEdifici.Items(I).Text & "</a></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"

                        ListaSelezionati.Add(ListEdifici.Items(I).Value.ToString)

                        If COLORE = "#FFFFFF" Then
                            COLORE = "#E6E6E6"
                        Else
                            COLORE = "#FFFFFF"
                        End If

                    End If
                Next
                lblEdifici.Text = testoTabella & "</table>"
                Session("ListaSelezionati") = ListaSelezionati

            End If

            Me.ScegEdifVis.Value = 1

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "addSelEdifici - " & ex.Message
        End Try

    End Sub
    Public Function Selezionati(ByVal lista As CheckBoxList) As Boolean
        Selezionati = False
        Try
            Selezionati = False
            vSelezionati = ""
            For Each I As ListItem In lista.Items
                If I.Selected = True Then
                    Selezionati = True
                    vSelezionati = vSelezionati & I.Value & ","
                End If
            Next
            If vSelezionati <> "" Then
                vSelezionati = vSelezionati.Substring(0, vSelezionati.LastIndexOf(","))
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function
End Class
