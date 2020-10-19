
Partial Class Contratti_Proroga_AssDefinitiva_Proroga
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            SettaDataScad()
            txtDataScadNuova.Attributes.Add("onblur", "javascript:confronta_data(document.getElementById('txtDataScadAtt').value,document.getElementById('txtDataScadNuova').value);")
            txtDataScadNuova.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

    Private Sub SettaDataScad()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID =" & Request.QueryString("IDC")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtDataScadAtt.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA_RINNOVO"), ""))
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim assTemp As Boolean = False
            Dim idContratto As Long = 0

            If confermaProroga.Value = 1 Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                idContratto = Request.QueryString("IDC")
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContratto & " AND FL_ASSEGN_TEMP=1"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    assTemp = True
                End If
                myReader0.Close()


                If assTemp = True Then
                    If Not IsNothing(Session.Item("lIdConnessione")) Then
                        Dim par1 As New CM.Global
                        par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
                        par1.cmd = par1.OracleConn.CreateCommand()
                        par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)
                        ‘'par1.cmd.Transaction = par1.myTrans

                        par1.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET DATA_SCADENZA_RINNOVO = '" & par.AggiustaData(txtDataScadNuova.Text) & "',FL_PROROGA=1,DATA_PROROGA='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & idContratto
                        par1.cmd.ExecuteNonQuery()

                        par1.myTrans.Commit()
                        par1.myTrans = par1.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
                        par1.Dispose()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F188','')"
                    par.cmd.ExecuteNonQuery()

                    dataProrogata.Value = txtDataScadNuova.Text

                    'Response.Write("<script>alert('Operazione effettuata con successo!');opener.parent.main.prorogati.value = '" & StatoProroga.Value & "';self.close();window.opener.reload();</script>")
                    Response.Write("<script>alert('Operazione effettuata con successo!');</script>")
                    btnProcedi.Enabled = False

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Response.Write("<script>alert('Attenzione...Il contratto non risulta TEMPORANEO. Impossibile procedere!')</script>")
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
