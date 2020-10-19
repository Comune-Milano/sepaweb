
Partial Class ANAUT_DecidiProposte
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property SoloLettura() As String
        Get
            If Not (ViewState("par_SoloLettura") Is Nothing) Then
                Return CStr(ViewState("par_SoloLettura"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_SoloLettura") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("DECIDI_DEC") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            CaricaElenco()
        End If

    End Sub

    Function CaricaElenco()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim COLORE As String = "#E5E5E5"

            If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
                SoloLettura = "1"
            End If

            lblElenco.Text = "<table>"
            lblElenco.Text = lblElenco.Text & "<tr style='font-family: Arial; font-size: 10pt; font-weight: bold'><td>Protocollo</td><td>Nominativo</td><td>Data Proposta</td><td>Esito</td><td>Data Esito</td><td></td></tr>"


            par.cmd.CommandText = "select utenza_comp_nucleo.cognome,utenza_comp_nucleo.nome,utenza_dichiarazioni.*,utenza_prop_decadenza.id as idprop,utenza_prop_decadenza.DATA_PROPOSTA,DECODE(utenza_prop_decadenza.ESITO,0,'NON DEFINITO',1,'CONFERMATA',2,'RESPINTA') AS VALUTAZIONE,utenza_prop_decadenza.DATA_ESITO from utenza_dichiarazioni,utenza_comp_nucleo,utenza_prop_decadenza where utenza_comp_nucleo.progr=0 and utenza_comp_nucleo.id_dichiarazione=utenza_dichiarazioni.id and utenza_dichiarazioni.id=utenza_prop_decadenza.id_dichiarazione order by utenza_prop_decadenza.id desc"
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myRec.Read
                lblElenco.Text = lblElenco.Text & "<tr bgcolor='" & COLORE & "' style='font-family: Arial; font-size: 9pt'><td><a href='javascript:void(0)' onclick='Apri(" & par.IfNull(myRec("id"), "") & ");'>" & par.IfNull(myRec("PG"), "") & "</a></td><td>" & par.IfNull(myRec("COGNOME"), "") & " " & par.IfNull(myRec("NOME"), "") & "</td><td>" & par.FormattaData(par.IfNull(myRec("DATA_PROPOSTA"), "")) & "</td><td>" & myRec("VALUTAZIONE") & "</td><td>" & par.FormattaData(par.IfNull(myRec("DATA_ESITO"), "")) & "</td><td><a href='javascript:void(0)' onclick='Decidi(" & par.IfNull(myRec("idprop"), "") & "," & par.IfNull(myRec("id"), "") & ");'>Decidi</a></td></tr>"
                If COLORE = "#E5E5E5" Then
                    COLORE = "#FFFFFF"
                Else
                    COLORE = "#E5E5E5"
                End If
            Loop
            myRec.Close()
            lblElenco.Text = lblElenco.Text & "</table>"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        If procedi.Value = "1" Then
            If RadioButton1.Checked = True Then
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "update UTENZA_PROP_DECADENZA SET esito=1,data_esito='" & Format(Now, "yyyyMMdd") & "' where id=" & iddichiarazione.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    & "VALUES (" & id.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F82','','I')"
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Operazione Effettuata');</script>")


                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    CaricaElenco()

                Catch ex As Exception
                    lblErrore.Visible = True
                    lblErrore.Text = ex.Message
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
            End If
            If RadioButton2.Checked = True Then
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "update UTENZA_PROP_DECADENZA SET esito=2,data_esito='" & Format(Now, "yyyyMMdd") & "' where id=" & iddichiarazione.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    & "VALUES (" & id.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F81','','I')"
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Operazione Effettuata');</script>")


                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    CaricaElenco()

                Catch ex As Exception
                    lblErrore.Visible = True
                    lblErrore.Text = ex.Message
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
            End If
        End If
    End Sub
End Class
