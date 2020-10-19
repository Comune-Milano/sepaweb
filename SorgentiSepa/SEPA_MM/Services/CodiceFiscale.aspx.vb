Imports System.IO
Imports Telerik.Web.UI

Partial Class SERVICES_CodiceFiscale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Public version As String = Mid(System.Configuration.ConfigurationManager.AppSettings("Versione"), 10).ToString.Trim().Replace(" ", "")

    Private Sub SERVICES_CodiceFiscale_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("T")) Then
                If Request.QueryString("T").ToString <> "1" Then
                    btnImposta.Visible = False
                Else
                    HFControlImpostaCheck.Value = "1"
                End If
            Else
                btnImposta.Visible = False
            End If
            txtDataNascita.DateInput.Attributes.Add("onkeydown", "CalendarDatePickerHide(this, event);")
        End If
    End Sub
    Private Sub CaricaComuneNazione()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT SIGLA, NOME FROM COMUNI_NAZIONI WHERE COD = " & par.insDbValue(HFCodComune.Value, True)
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                acbComune.Entries.Insert(0, New AutoCompleteBoxEntry(par.IfNull(MyReader("NOME"), "")))
                txtProvincia.Text = par.IfNull(MyReader("SIGLA"), "")
            End If
            MyReader.Close()
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Function ControlloCodiceFiscale() As Boolean
        ControlloCodiceFiscale = False
        Dim Errori As String = ""
        If String.IsNullOrEmpty(Trim(txtCognome.Text)) Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- Definire il Cognome;"
            Else
                Errori &= "<br>" & "- Definire il Cognome;"
            End If
        Else
            txtCognome.Text = txtCognome.Text.Trim.ToUpper
        End If
        If String.IsNullOrEmpty(Trim(txtNome.Text)) Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- Definire il Nome;"
            Else
                Errori &= "<br>" & "- Definire il Nome;"
            End If
        Else
            txtNome.Text = txtNome.Text.Trim.ToUpper
        End If
        If String.IsNullOrEmpty(Trim(HFCodComune.Value.ToString)) Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- Definire il Luogo di Nascita;"
            Else
                Errori &= "<br>" & "- Definire il Luogo di Nascita;"
            End If
        End If
        If IsNothing(txtDataNascita.SelectedDate) Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- Definire la Data di Nascita;"
            Else
                Errori &= "<br>" & "- Definire la Data di Nascita;"
            End If
        End If
        If String.IsNullOrEmpty(Trim(Errori)) Then
            ControlloCodiceFiscale = True
        Else
            par.MessaggioAlert(CType(Me.Page.FindControl("RadWindowManagerMaster"), RadWindowManager), Errori, 450, 150, "Attenzione", Nothing, Nothing)
        End If
    End Function
    Private Sub btnCalcola_Click(sender As Object, e As EventArgs) Handles btnCalcola.Click
        Try
            If ControlloCodiceFiscale() Then
                Dim DataNascita As String = ""
                If Not IsNothing(txtDataNascita.SelectedDate) Then DataNascita = par.FormattaData(txtDataNascita.SelectedDate)
                txtCodiceFiscale.Text = par.CalcolaCodiceFiscale(txtCognome.Text, txtNome.Text, DataNascita, HFCodComune.Value.ToString, par.IfEmpty(ddlSesso.SelectedValue.ToString, "M"))
            End If
        Catch ex As Exception
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtCodiceFiscale.Text = ""
            txtCognome.Text = ""
            txtNome.Text = ""
            par.setComboTelerik(ddlSesso, "M")
            acbComune.Entries.Clear()
            txtProvincia.Text = ""
            txtDataNascita.SelectedDate = Nothing
        Catch ex As Exception
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
