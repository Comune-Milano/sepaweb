
Partial Class Contratti_InserimMotivSpostAnnull
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            caricaMotivazioni()
            CaricaCompetenze()
        End If

    End Sub



    Private Sub CaricaCompetenze()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            rdbCompetenze.Items.Clear()
            par.cmd.CommandText = "select * from siscom_mi.COMPETENZE_SPESE order by id asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                rdbCompetenze.Items.Add(New ListItem(par.IfNull(myReader("nome"), " "), par.IfNull(myReader("id"), -1)))
            End While
            myReader.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    Private Sub caricaMotivazioni()
        Try


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim sStringaSQL As String = ""

            sStringaSQL = " SELECT motivi_spostam_annull.ID, motivi_spostam_annull.id_competenza as id_comp, " _
                        & " motivi_spostam_annull.motivazione, competenze_spese.nome AS competenza " _
                        & " FROM siscom_mi.competenze_spese, siscom_mi.motivi_spostam_annull " _
                        & " WHERE competenze_spese.ID = motivi_spostam_annull.id_competenza order by id "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            DataGrMotivazioni.DataSource = dt


            DataGrMotivazioni.DataBind()

       


            txtmia.Text = "Nessuna Selezione"


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception


            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub




    Protected Sub DataGrMotivazioni_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrMotivazioni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la motivazione " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la motivazione " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

   

    Protected Sub btnModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click

        Try


            lbl_titMotiv.Text = "Modifica Motivazione"

            If LBLID.Value <> "0" Then
                ''*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.motivi_spostam_annull WHERE ID = " & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    txtMotivazione.Text = par.IfNull(myReader("MOTIVAZIONE"), "")
                    rdbCompetenze.SelectedValue = par.IfNull(myReader("ID_COMPETENZA"), -1)
                End If
                myReader.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Me.Modificato.Value = 2
            Else
                '  Me.Modificato.Value = 0
                ' Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            End If



        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Sub

    Protected Sub btn_chiudi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_chiudi.Click
        txtMotivazione.Text = ""
        rdbCompetenze.SelectedIndex = -1
        LBLID.Value = 0
        Me.txtmia.Text = "Nessuna Selezione"
        Modificato.Value = "0"
        lbl_titMotiv.Text = "Nuova Motivazione"
    End Sub

    Protected Sub btn_inserisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_inserisci.Click
        Try

            Dim scriptblock As String = ""

            If txtMotivazione.Text = "" Then

                scriptblock = "<script language='javascript' type='text/javascript'> alert('Inserire la motivazione!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If


                Exit Try
            End If

            If rdbCompetenze.SelectedValue = "" Then
                scriptblock = "<script language='javascript' type='text/javascript'> alert('Selezionare competenza!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If

                Exit Try
            End If


            If Modificato.Value <> "2" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "INSERT INTO siscom_mi.motivi_spostam_annull (ID,ID_COMPETENZA,MOTIVAZIONE) VALUES (SISCOM_MI.SEQ_MOTIVI_SPOSTAM_ANNULL.NEXTVAL," & rdbCompetenze.SelectedValue & ",'" & par.PulisciStrSql(txtMotivazione.Text).ToUpper & "')"
                par.cmd.ExecuteNonQuery()

                scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Modificato.Value = "0"


                txtMotivazione.Text = ""
                rdbCompetenze.SelectedIndex = -1
            caricaMotivazioni()
            Else
            Update()
            End If



        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub Update()
        Try


            Dim scriptblock As String = ""


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            If txtMotivazione.Text = "" Then

                scriptblock = "<script language='javascript' type='text/javascript'> alert('Inserire la motivazione!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If


                Exit Try
            End If


            par.cmd.CommandText = "UPDATE siscom_mi.motivi_spostam_annull SET ID_COMPETENZA= " & rdbCompetenze.SelectedValue & ",MOTIVAZIONE = '" & par.PulisciStrSql(txtMotivazione.Text).ToUpper & "' WHERE ID = " & LBLID.Value
            par.cmd.ExecuteNonQuery()

            scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Modificato.Value = "0"
            caricaMotivazioni()
            LBLID.Value = 0
            txtmia.Text = "Nessuna Selezione"
            txtMotivazione.Text = ""
            rdbCompetenze.SelectedIndex = -1




        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub btnElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Try

            Dim scriptblock As String = ""

            If ConfElimina.Value = 1 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RU_CAMBIO_UI WHERE ID_MOTIVAZIONE =" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    scriptblock = "<script language='javascript' type='text/javascript'>alert('Non è possibile cancellare il dato selezionato!');</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                    End If
                Else

                    par.cmd.CommandText = "DELETE FROM siscom_mi.motivi_spostam_annull WHERE ID = " & LBLID.Value
                    par.cmd.ExecuteNonQuery()

                    scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                    End If


                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
                Modificato.Value = 0
                txtmia.Text = "Nessuna Selezione"
                LBLID.Value = 0
                ConfElimina.Value = 0
                DataGrMotivazioni.CurrentPageIndex = 0
                caricaMotivazioni()


        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub DataGrMotivazioni_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrMotivazioni.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrMotivazioni.CurrentPageIndex = e.NewPageIndex
            caricaMotivazioni()

        End If
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
