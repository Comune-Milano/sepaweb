
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_ElencoOp
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim ElencoOperatori As String()
    Dim kk As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Label2.Text = Request.QueryString("T")

                If Len(Request.QueryString("V")) > 1 Then
                    par.cmd.CommandText = "select * from operatori where id in (" & Mid(Replace(Request.QueryString("V"), "x", ","), 1, Len(Request.QueryString("V")) - 1) & ") order by operatore asc"
                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader5.Read
                        Label4.Text = Label4.Text & par.IfNull(myReader5("operatore"), "") & "<br />"
                    Loop
                    myReader5.Close()
                Else
                    Label4.Text = "Nessun operatore è stato associato a questa voce!"
                    Label6.Text = "* Attenzione, è necessario associare almeno un operatore alla funzione, altrimenti il piano finanziario non potrà passare allo stato -COMPILAZIONE IMPORTI-. Si ricorda che l'associazione voce/operatori NON potrà essere fatta dopo che lo schema sarà dichiarazto -COMPLETO-."
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try

        End If

    End Sub

    'Function Operatori(ByVal testo As String)
    '    Dim pos As Integer
    '    Dim Valore1 As String

    '    pos = 1
    '    Valore1 = ""
    '    Do While pos <= Len(testo)
    '        If Mid(testo, pos, 1) <> "," Then
    '            Valore1 = Valore1 & Mid(testo, pos, 1)
    '        Else
    '            ReDim Preserve ElencoOperatori(kk)
    '            ElencoOperatori(kk) = Valore1
    '            kk = kk + 1
    '            Valore1 = ""
    '        End If
    '        pos = pos + 1
    '    Loop
    '    pos = pos + 1
    'End Function
End Class
