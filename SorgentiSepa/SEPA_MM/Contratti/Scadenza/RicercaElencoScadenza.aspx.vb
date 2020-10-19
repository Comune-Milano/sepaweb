
Partial Class Contratti_Scadenza_RicercaElencoScadenza
    Inherits PageSetIdMode
    Dim sStringaSQL As String = ""
    Dim par As New CM.Global
    Dim comuniMI As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        If Not IsPostBack Then
            CaricaDati()
        End If

    End Sub

    Private Sub CaricaDati()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Dim Stringa As String = ""




            'Me.ddl_comuni.Items.Add(New ListItem("TUTTI", "-1"))


            Stringa = "SELECT DISTINCT ID, comuni_nazioni.nome FROM siscom_mi.unita_contrattuale, sepa.comuni_nazioni WHERE cod=cod_comune order by nome"


            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Stringa, par.OracleConn)

            Dim ds As New Data.DataTable

            da1.Fill(ds)

            For Each row As Data.DataRow In ds.Rows
                Me.chComuni.Items.Add(New ListItem(par.IfNull(row.Item("NOME"), " "), par.IfNull(row.Item("ID"), -1)))
            Next

            ds = New Data.DataTable
            da1.Dispose()











            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try


    End Sub




    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim sData As String = ""
            Dim sComune As String = ""

            If par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null" Then



                If (par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null") Then
                    sData = par.AggiustaData(Me.txtDataDal.Text)
                    '    sStringaSQL = sStringaSQL & " AND DATA_SCADENZA_RINNOVO >= " & sValore
                End If



                Dim primo As Boolean = False
                For Each chcom As ListItem In chComuni.Items
                    If chcom.Selected = True Then
                        If primo = False Then
                            comuniMI = chcom.Value & ","
                            primo = True
                        Else
                            comuniMI = comuniMI & chcom.Value & ","
                        End If
                    End If
                Next



                If comuniMI = "" Then
                    Response.Write("<script>alert('Selezionare almeno un Comune di riferimento');</script>")
                    Exit Sub
                ElseIf comuniMI <> "" Then
                    comuniMI = Mid(comuniMI, 1, Len(comuniMI) - 1)
                End If

                Response.Write("<script>window.open('RptElencoScadenza.aspx?DATA=" & sData & "&COM=" & comuniMI & "', 'RptScadenza', 'menubar=no, scrollbars=yes, width=1500,height=800, resizable=yes resizable = yes');</script>")
               
            Else
                Response.Write("<script>alert('Definire la data di riferimento scadenza!');</script>")


            End If

            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Redirect("../pagina_home.aspx")

    End Sub

    Protected Sub chSelezione_CheckedChanged(sender As Object, e As System.EventArgs) Handles chSelezione.CheckedChanged
        If chSelezione.Checked = True Then
            For Each li As ListItem In chComuni.Items
                li.Selected = True
            Next
        Else
            For Each li As ListItem In chComuni.Items
                li.Selected = False
            Next
        End If
    End Sub
End Class
