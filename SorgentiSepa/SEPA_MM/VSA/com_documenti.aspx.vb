
Partial Class ANAUT_com_documenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If Not IsPostBack = True Then

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            If Not String.IsNullOrEmpty(COMPONENTE) Then
                Riempi(COMPONENTE)
            Else
                RiempiDaDb(Request.QueryString("DIC"))
            End If
            RiempiTipologia()


        End If


    End Sub

    Private Sub RiempiTipologia()
        Try
            par.OracleConn.Open()

            par.SettaCommand(par)

            'par.cmd.CommandText = "select id,descrizione from vsa_doc_necessari where id in (select id_documento from vsa_doc_tipo_necessari " _
            '    & " where id_tipo_domanda = " & Request.QueryString("TDOM") & ") order by descrizione asc"

            par.cmd.CommandText = "select id,descrizione from vsa_doc_necessari order by descrizione asc"

            Dim myRec55 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myRec55.Read
                cmbTipo.Items.Add(New ListItem(myRec55("DESCRIZIONE"), myRec55("ID")))
            Loop
            myRec55.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Function Riempi(ByVal testo As String)
        Dim pos As Integer
        Dim Valore1 As String
        Dim Valore2 As String

        pos = 1

        Valore1 = ""
        Valore2 = ""
        Do While pos <= Len(testo)
            If Mid(testo, pos, 1) <> ";" Then
                Valore1 = Valore1 & Mid(testo, pos, 1)
            Else
                pos = pos + 1
                Do While pos <= Len(testo)
                    If Mid(testo, pos, 1) <> "!" Then
                        Valore2 = Valore2 & Mid(testo, pos, 1)
                    Else
                        cmbComponente.Items.Add(New ListItem(Valore1, Valore2))
                        Valore1 = ""
                        Valore2 = ""
                        Exit Do
                    End If
                    pos = pos + 1
                Loop
            End If
            pos = pos + 1
        Loop

        cmbComponente.Items.Add(New ListItem("--", "-1"))
        cmbTipo.ClearSelection()
        cmbComponente.Items.FindByText("--").Selected = True
    End Function
    Private Sub RiempiDaDb(ByVal idDichiarazione As String)
        Try
            Dim idDomanda As String = ""
            par.OracleConn.Open()
            par.SettaCommand(par)

            If Request.QueryString("TDOM") <> "7" Then
                par.cmd.CommandText = "select id,(cognome||' '||nome) as componente from comp_nucleo_vsa where id_dichiarazione = " & idDichiarazione & " order by progr asc"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                cmbComponente.Items.Add(New ListItem("--", "-1"))
                While lettore.Read
                    Me.cmbComponente.Items.Add(New ListItem(par.IfNull(lettore("componente"), ""), par.IfNull(lettore("id"), 0)))
                End While
                lettore.Close()
            Else
                par.cmd.CommandText = "select id from domande_bando_vsa where id_dichiarazione = " & idDichiarazione
                Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore2.Read Then
                    idDomanda = lettore2("id")
                End If
                lettore2.Close()

                par.cmd.CommandText = "select id,(cognome||' '||nome) as componente from comp_nucleo_ospiti_vsa where id_domanda = " & idDomanda & " order by cognome asc"
                Dim lettore3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                cmbComponente.Items.Add(New ListItem("--", "-1"))
                While lettore3.Read
                    Me.cmbComponente.Items.Add(New ListItem(par.IfNull(lettore3("componente"), ""), par.IfNull(lettore3("id"), 0)))
                End While
                lettore3.Close()
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbTipo.SelectedItem.Value
        If cmbComponente.SelectedItem.Text = "--" Then
            Cache(Session.Item("GLista")) = par.MiaFormat(cmbTipo.SelectedItem.Text & " " & UCase(txtAltro.Text), 250)
        Else
            Cache(Session.Item("GLista")) = par.MiaFormat(cmbTipo.SelectedItem.Text & " del Sig./Sig.ra " & cmbComponente.SelectedItem.Text & " " & UCase(txtAltro.Text), 250)
        End If


        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub
End Class
