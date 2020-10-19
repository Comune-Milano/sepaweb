
Partial Class CENSIMENTO_Tab_PavimRivest
    Inherits UserControlSetIdMode
    Dim par As New CM.Global




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If DirectCast(Me.Page.FindControl("id_stato"), HiddenField).Value = 2 Then

            sola_lettura.Value = 1

        End If

        If Not IsPostBack Then

            ControllaStato()


            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.dgDatiUI.Items.Count - 1
                di = Me.dgDatiUI.Items(i)

                If CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "" Or CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = 0 Then

                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"

                Else
                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = FormatNumber(CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text, 2)


                End If


            Next









        End If

       



    End Sub


    Private Sub CalcolaTotAddebito()

        Try


            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.dgDatiUI.Items.Count - 1
                di = Me.dgDatiUI.Items(i)



                If CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "" Or CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = 0 Then

                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"





                End If

                If CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Text = "" Then

                    CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Text = 0


                End If





                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);")
                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onfocusin", "javascript:SettaTestoOncosto(" & CType(di.Cells(15).FindControl("addebito_txt"), TextBox).ClientID & ");")
                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onfocusin", "javascript:SettaTestoOnquant(" & CType(di.Cells(13).FindControl("quantita_txt"), TextBox).ClientID & ");")

                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onfocusout", "javascript:SettaTestoOutcosto(" & CType(di.Cells(15).FindControl("addebito_txt"), TextBox).ClientID & ");")
                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onfocusout", "javascript:SettaTestoOutquant(" & CType(di.Cells(13).FindControl("quantita_txt"), TextBox).ClientID & ");")



                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onblur", "javascript:CalcolaAddebito(" & CType(di.Cells(13).FindControl("quantita_txt"), TextBox).ClientID & ", " & (Left(di.Cells(14).Text, Len(di.Cells(14).Text) - 2)).Replace(",", ".") & ", " & CType(di.Cells(15).FindControl("addebito_txt"), TextBox).ClientID & "); javascript:CalcolaTotale();")




            Next

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

    Private Sub generaTable()

        Try


            Dim dt As New Data.DataTable()



            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
           & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
           & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
           & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
           & " WHERE sl_tipo_st_manut.tab_appartenenza = 4 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)



            dgDatiUI.DataSource = dt
            dgDatiUI.DataBind()









            For K As Integer = 0 To dgDatiUI.Items.Count - 1




                If dgDatiUI.Items(K).Cells(2).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False


                End If




                If dgDatiUI.Items(K).Cells(3).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False


                End If



                If dgDatiUI.Items(K).Cells(4).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False


                End If



                If dgDatiUI.Items(K).Cells(5).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False


                End If



            Next

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


    Private Sub impostaValori()

        Try


            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.dgDatiUI.Items.Count - 1
                di = Me.dgDatiUI.Items(i)





                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"



                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Text = 0







                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);")
                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onfocusin", "javascript:SettaTestoOncosto(" & CType(di.Cells(15).FindControl("addebito_txt"), TextBox).ClientID & ");")
                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onfocusin", "javascript:SettaTestoOnquant(" & CType(di.Cells(13).FindControl("quantita_txt"), TextBox).ClientID & ");")

                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Attributes.Add("onfocusout", "javascript:SettaTestoOutcosto(" & CType(di.Cells(15).FindControl("addebito_txt"), TextBox).ClientID & ");")
                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onfocusout", "javascript:SettaTestoOutquant(" & CType(di.Cells(13).FindControl("quantita_txt"), TextBox).ClientID & ");")



                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Attributes.Add("onblur", "javascript:CalcolaAddebito(" & CType(di.Cells(13).FindControl("quantita_txt"), TextBox).ClientID & ", " & (Left(di.Cells(14).Text, Len(di.Cells(14).Text) - 2)).Replace(",", ".") & ", " & CType(di.Cells(15).FindControl("addebito_txt"), TextBox).ClientID & "); javascript:CalcolaTotale();")




            Next

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




    Private Sub caricaTable()


        Try



            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim dt As New Data.DataTable()

            par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
           & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
           & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
           & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
           & " WHERE sl_tipo_st_manut.tab_appartenenza = 4 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)



            dgDatiUI.DataSource = dt
            dgDatiUI.DataBind()









            For K As Integer = 0 To dgDatiUI.Items.Count - 1




                If dgDatiUI.Items(K).Cells(2).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False


                End If




                If dgDatiUI.Items(K).Cells(3).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False


                End If



                If dgDatiUI.Items(K).Cells(4).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False


                End If



                If dgDatiUI.Items(K).Cells(5).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False


                End If



            Next




            For K As Integer = 0 To dgDatiUI.Items.Count - 1

                par.cmd.CommandText = ""

                par.cmd.CommandText = "SELECT SL_RAPPORTO.ID_SLOGGIO, SL_RAPPORTO.ID_DESC_ST_MANUT, SL_RAPPORTO.QUANTITA, SL_RAPPORTO.TOTALE, " _
                                      & " (CASE WHEN SL_RAPPORTO.CHK_1 IS NULL THEN '0' ELSE TO_CHAR(CHK_1) END) as CHK_1, " _
                                      & " (CASE WHEN SL_RAPPORTO.CHK_2 IS NULL THEN '0' ELSE TO_CHAR(CHK_2) END) as CHK_2, " _
                                      & " (CASE WHEN SL_RAPPORTO.CHK_3 IS NULL THEN '0' ELSE TO_CHAR(CHK_3) END) as CHK_3, " _
                                      & " (CASE WHEN SL_RAPPORTO.CHK_4 IS NULL THEN '0' ELSE TO_CHAR(CHK_4) END) as CHK_4 " _
                                      & " FROM SISCOM_MI.SL_RAPPORTO " _
                                      & " WHERE SL_RAPPORTO.ID_SLOGGIO = " & DirectCast(Me.Page.FindControl("id_sloggio"), HiddenField).Value & " AND  SL_RAPPORTO.ID_DESC_ST_MANUT = " & dgDatiUI.Items(K).Cells(1).Text & ""


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    '
                    If par.IfNull(myReader1("CHK_1"), "") <> "0" Then


                        DirectCast(dgDatiUI.Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True

                    End If


                    If par.IfNull(myReader1("CHK_2"), "") <> "0" Then


                        DirectCast(dgDatiUI.Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True

                    End If


                    If par.IfNull(myReader1("CHK_3"), "") <> "0" Then


                        DirectCast(dgDatiUI.Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True

                    End If


                    If par.IfNull(myReader1("CHK_4"), "") <> "0" Then


                        DirectCast(dgDatiUI.Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True

                    End If



                    DirectCast(dgDatiUI.Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text = par.IfNull(myReader1("QUANTITA"), "")
                    DirectCast(dgDatiUI.Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text = par.IfNull(myReader1("TOTALE"), "")





                End If



            Next

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

            'par.cmd.CommandText = "SELECT SL_SLOGGIO.STATO_VERBALE AS ST_VERB FROM SISCOM_MI.SL_SLOGGIO WHERE SL_SLOGGIO.ID = " & DirectCast(Me.Page.FindControl("id_sloggio"), HiddenField).Value

            'Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader3.Read Then
            '    '
            '    stato_verb.Value = par.IfNull(myReader3("ST_VERB"), "")




            'End If

            'myReader3.Close()


            If DirectCast(Me.Page.FindControl("id_stato"), HiddenField).Value >= 0 And DirectCast(Me.Page.FindControl("stato_verb"), HiddenField).Value = 0 Then

                generaTable()
                impostaValori()

            Else
                caricaTable()
                CalcolaTotAddebito()


                If sola_lettura.Value = 1 Then

                    For K As Integer = 0 To dgDatiUI.Items.Count - 1

                        DirectCast(dgDatiUI.Items(K).Cells(8).FindControl("stato1"), CheckBox).Enabled = False
                        DirectCast(dgDatiUI.Items(K).Cells(9).FindControl("stato2"), CheckBox).Enabled = False
                        DirectCast(dgDatiUI.Items(K).Cells(10).FindControl("stato3"), CheckBox).Enabled = False
                        DirectCast(dgDatiUI.Items(K).Cells(11).FindControl("stato4"), CheckBox).Enabled = False
                        DirectCast(dgDatiUI.Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Enabled = False
                        DirectCast(dgDatiUI.Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Enabled = False



                    Next

                End If




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



End Class
