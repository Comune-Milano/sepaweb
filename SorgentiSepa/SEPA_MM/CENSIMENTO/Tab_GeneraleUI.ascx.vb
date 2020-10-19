
Partial Class CENSIMENTO_Tab_GeneraleUI
    Inherits UserControlSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'Me.chk_scivoli.Attributes.Add("onclick", "javascript:AbilitaDropScivoli(this.checked);")
        'Me.chk_montascale.Attributes.Add("onclick", "javascript:AbilitaDropMontascale(this.checked);")
        'Me.chk_esistente.Attributes.Add("onclick", "javascript:AbilitaDropEsistente(this.checked);")
        Me.chk_locale.Attributes.Add("onclick", "javascript:AbilitaTxt(this.checked);")

       

        If DirectCast(Me.Page.FindControl("id_stato"), HiddenField).Value = 2 Then
            sola_lettura.Value = 1
        End If

        If Not IsPostBack Then
            ControllaStato()
        End If
    End Sub

    Private Sub caricaDatiNuovo()
        Try
            Dim dt As New Data.DataTable()
            '*****************APERTURA CONNESSIONE***************


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT unita_immobiliari.cod_unita_immobiliare, unita_immobiliari.COD_STATO_CONS_LG_392_78, edifici.num_ascensori " _
                                  & " FROM siscom_mi.unita_immobiliari, siscom_mi.edifici" _
                                  & "  WHERE edifici.ID = unita_immobiliari.id_edificio " _
                                  & "And unita_immobiliari.ID = " & DirectCast(Me.Page.FindControl("idunita"), HiddenField).Value & ""

            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then



                If par.IfNull(myReader3("NUM_ASCENSORI"), "-1") > 0 Then

                    ddl_ascensore.SelectedValue = 1
                Else

                    ddl_ascensore.SelectedValue = 0

                End If

                If par.IfNull(myReader3("COD_STATO_CONS_LG_392_78"), "") = "SCADE" Then

                    ddl_statocons.SelectedValue = 2

                ElseIf par.IfNull(myReader3("COD_STATO_CONS_LG_392_78"), "") = "MEDIO" Then

                    ddl_statocons.SelectedValue = 1


                ElseIf par.IfNull(myReader3("COD_STATO_CONS_LG_392_78"), "") = "NORMA" Then

                    ddl_statocons.SelectedValue = 0

                Else

                    ddl_statocons.SelectedValue = -1

                End If


            End If
            myReader3.Close()





            If sola_lettura.Value = 1 Then
                DisattivaTutto()
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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub ControllaStato()
        Try
            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If DirectCast(Me.Page.FindControl("id_stato"), HiddenField).Value >= 0 And DirectCast(Me.Page.FindControl("stato_verb"), HiddenField).Value = 0 Then

                caricaDatiNuovo()
            Else
                caricaDati()
            End If
            SettaStatoControl()


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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub SettaStatoControl()

    

        If chk_locale.Checked = True Then

            txt_locale.Enabled = True
        Else
            txt_locale.Enabled = False

        End If
    End Sub


    Private Sub caricaDati()
        Try
            Dim dt As New Data.DataTable()
            '*****************APERTURA CONNESSIONE***************


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT ASCENSORE, SCIVOLI, MONTASCALE, FORO_AREAZIONE, LOCALE_FORO_AREAZ, COD_STATO_CONSERV, LIVELLO, RECUPERABILE " _
                                  & " FROM siscom_mi.SL_SLOGGIO " _
                                  & " WHERE SL_SLOGGIO.ID = " & DirectCast(Me.Page.FindControl("id_sloggio"), HiddenField).Value & ""

            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then



                If par.IfNull(myReader3("ASCENSORE"), "") > 0 Then

                    ddl_ascensore.SelectedValue = 1
                ElseIf par.IfNull(myReader3("ASCENSORE"), "") = 0 Then

                    ddl_ascensore.SelectedValue = 0

                Else

                    ddl_ascensore.SelectedValue = -1

                End If




                If par.IfNull(myReader3("SCIVOLI"), "") > 0 Then

                    chk_scivoli.Checked = True
                Else

                    chk_scivoli.Checked = False

                End If





                If par.IfNull(myReader3("MONTASCALE"), "") > 0 Then

                    chk_montascale.Checked = True
                Else
                    chk_montascale.Checked = False

                End If




                If par.IfNull(myReader3("FORO_AREAZIONE"), "") > 0 Then

                    chk_esistente.Checked = True
                Else
                    chk_esistente.Checked = False

                End If




                If par.IfNull(myReader3("LOCALE_FORO_AREAZ"), "") <> "" Then

                    chk_locale.Checked = True
                    txt_locale.Text = par.IfNull(myReader3("LOCALE_FORO_AREAZ"), "")

                Else
                    chk_locale.Checked = False

                End If





                If par.IfNull(myReader3("COD_STATO_CONSERV"), "") = "SCADE" Then

           ddl_statocons.SelectedValue = 2

                ElseIf par.IfNull(myReader3("COD_STATO_CONSERV"), "") = "MEDIO" Then

                    ddl_statocons.SelectedValue = 1


                ElseIf par.IfNull(myReader3("COD_STATO_CONSERV"), "") = "NORMA" Then

                    ddl_statocons.SelectedValue = 0

                Else

                    ddl_statocons.SelectedValue = -1

                End If








                If par.IfNull(myReader3("LIVELLO"), "") = "BASSO" Then

                    ddl_livello.SelectedValue = 0

                ElseIf par.IfNull(myReader3("LIVELLO"), "") = "MEDIO" Then

                    ddl_livello.SelectedValue = 1


                ElseIf par.IfNull(myReader3("LIVELLO"), "") = "ALTO" Then

                    ddl_livello.SelectedValue = 2

                Else

                    ddl_livello.SelectedValue = -1

                End If


                If par.IfNull(myReader3("RECUPERABILE"), "") = 0 Then

                    ddl_UIRecuperabile.SelectedValue = 0
                ElseIf par.IfNull(myReader3("RECUPERABILE"), "") = 1 Then

                    ddl_UIRecuperabile.SelectedValue = 1

                Else

                    ddl_UIRecuperabile.SelectedValue = -1

                End If

            End If
            myReader3.Close()




            If sola_lettura.Value = 1 Then
                DisattivaTutto()
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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub DisattivaTutto()

        ddl_ascensore.Enabled = False
        chk_scivoli.Enabled = False
        chk_montascale.Enabled = False
        chk_esistente.Enabled = False
        chk_locale.Enabled = False
        txt_locale.Enabled = False
        ddl_statocons.Enabled = False
        ddl_livello.Enabled = False
        ddl_UIRecuperabile.Enabled = False



    End Sub









End Class
