
Partial Class Contratti_Prorogati_RicercaElencoProrogati
    Inherits PageSetIdMode
    Dim sStringaSQL As String = ""
    Dim par As New CM.Global
    Dim comuniMI As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Me.txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
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




            par.RiempiDList(Me, par.OracleConn, "ddl_Filiale", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID > 0 AND ID <10 OR ID = 22 ORDER BY NOME ASC", "NOME", "ID")
            ddl_Filiale.Items.Add("TUTTI")
            ddl_Filiale.Items.FindByText("TUTTI").Selected = True







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
            Dim sDataDal As String = ""
            Dim sDataAl As String = ""
            Dim sComune As String = ""
            Dim sFiliale As String = ""

            If par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null" Or par.IfEmpty(Me.txtDataAl.Text, "Null") <> "Null" Then



                If (par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null") Then
                    sDataDal = par.AggiustaData(Me.txtDataDal.Text)

                End If



                If (par.IfEmpty(Me.txtDataAl.Text, "Null") <> "Null") Then
                    sDataAl = par.AggiustaData(Me.txtDataAl.Text)

                End If


                If ddl_Filiale.Items.FindByText("TUTTI").Selected = True Then
                    sFiliale = ""
                Else
                    sFiliale = ddl_Filiale.SelectedItem.Value
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

                Response.Write("<script>window.open('RptElencoProrogati.aspx?DATADAL=" & sDataDal & "&DATAAL=" & sDataAl & "&FIL=" & sFiliale & "&COM=" & comuniMI & "', 'RptProrogati', 'menubar=no, scrollbars=yes, width=1500,height=800, resizable=yes resizable = yes');</script>")

            Else
                Response.Write("<script>alert('Definire almeno l\'inizio dell\'intervallo di tempo relativo alla proroga dei contratti!');</script>")


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

