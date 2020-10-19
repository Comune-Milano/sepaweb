
Partial Class Contabilita_CicloPassivo_Plan_Lotto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If cmbLotti.SelectedIndex <> -1 Then
            VerificaLotto()
            'Response.Redirect("VociServizio.aspx?IDV=" & idVoce.Value & "&IDL=" & cmbLotti.SelectedItem.Value & "&IDS=" & idServizio.Value & "&IDP=" & idPianoF.Value)
            'End If
        Else
        Response.Write("<script>alert('Attenzione...Devi selezionare un lotto dalla lista.\nE\' possibile creare nuovi lotti premendo sul pulsante +');</script>")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idPianoF.Value = Request.QueryString("IDP")
            idVoce.Value = Request.QueryString("IDV")
            idServizio.Value = Request.QueryString("IDS")

            CaricaStato()
            CaricaServizio()
            CaricaLotti()
            ServiziAssociati()
            If cmbLotti.SelectedIndex >= 0 Then
                selezionato.Value = cmbLotti.SelectedItem.Value
            End If


        End If

        'ServiziAssociati()
    End Sub


    Function VerificaLotto() As Boolean
        Try
            VerificaLotto = False
            If conferma.Value = "1" Then
                If cista.Value = "0" Then

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "insert into siscom_mi.lotti_servizi (id_lotto,id_servizio) values (" & cmbLotti.SelectedItem.Value & "," & idServizio.Value & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                End If
                If tipolotto.Value = "E" Then
                    Response.Redirect("VociServizio.aspx?T=E&IDV=" & idVoce.Value & "&IDL=" & cmbLotti.SelectedItem.Value & "&IDS=" & idServizio.Value & "&IDP=" & idPianoF.Value)
                Else
                    Response.Redirect("VociServizio.aspx?T=I&IDV=" & idVoce.Value & "&IDL=" & cmbLotti.SelectedItem.Value & "&IDS=" & idServizio.Value & "&IDP=" & idPianoF.Value)
                End If
            End If


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function



    Function CaricaLotti()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim TPO_L As String = "E"
            Dim ID_ESERCIZIO_F As Long = 0

            par.cmd.CommandText = "select * FROM siscom_mi.PF_MAIN WHERE ID=" & idPianoF.Value
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                ID_ESERCIZIO_F = myReader2("ID_ESERCIZIO_FINANZIARIO")
            End If
            myReader2.Close()
            cmbLotti.Items.Clear()

            If Session.Item("LIVELLO") = "1" Then
                'par.RiempiDList(Me, par.OracleConn, "cmbLotti", "select lotti.* from siscom_mi.lotti where lotti.id_esercizio_finanziario=" & ID_ESERCIZIO_F & " order by lotti.descrizione asc", "DESCRIZIONE", "ID")
                par.cmd.CommandText = "select lotti.* from siscom_mi.lotti where lotti.tipo<>'X' AND lotti.id_esercizio_finanziario=" & ID_ESERCIZIO_F & " order by lotti.descrizione asc"
            Else
                'par.RiempiDList(Me, par.OracleConn, "cmbLotti", "select lotti.* from siscom_mi.lotti,operatori where operatori.id=" & Session.Item("ID_OPERATORE") & "  AND lotti.ID_FILIALE=OPERATORI.ID_UFFICIO and lotti.id_esercizio_finanziario=" & ID_ESERCIZIO_F & " order by lotti.descrizione asc", "DESCRIZIONE", "ID")
                par.cmd.CommandText = "select lotti.* from siscom_mi.lotti,operatori where lotti.tipo<>'X' AND operatori.id=" & Session.Item("ID_OPERATORE") & "  AND lotti.ID_FILIALE=OPERATORI.ID_UFFICIO and lotti.id_esercizio_finanziario=" & ID_ESERCIZIO_F & " order by lotti.descrizione asc"
            End If

            myReader2 = par.cmd.ExecuteReader()
            Do While myReader2.Read
                If par.IfNull(myReader2("TIPO"), "E") = "E" Then
                    TPO_L = "E-"
                Else
                    TPO_L = "I-"
                End If
                cmbLotti.Items.Add(New ListItem(TPO_L & par.IfNull(myReader2("DESCRIZIONE"), ""), myReader2("ID")))
            Loop
            myReader2.Close()



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function CaricaServizio()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select * from siscom_mi.TAB_SERVIZI where id=" & idServizio.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = par.IfNull(myReader5("descrizione"), "")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function


    Function ServiziAssociati()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            cista.Value = "0"
            lblServiziAssociati.Text = ""
            If cmbLotti.SelectedIndex <> -1 Then
                par.cmd.CommandText = "select TAB_SERVIZI.descrizione,tab_servizi.id from siscom_mi.TAB_SERVIZI,siscom_mi.lotti_servizi where lotti_servizi.id_servizio=tab_servizi.id and lotti_servizi.id_lotto=" & cmbLotti.SelectedItem.Value
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader5.Read
                    lblServiziAssociati.Text = lblServiziAssociati.Text & par.IfNull(myReader5("descrizione"), "") & ";<br/>"
                    If par.IfNull(myReader5("id"), "-1") = idServizio.Value Then
                        cista.Value = "1"
                    End If
                Loop
                myReader5.Close()

                par.cmd.CommandText = "select tipo from siscom_mi.lotti where id=" & cmbLotti.SelectedItem.Value
                myReader5 = par.cmd.ExecuteReader()
                If myReader5.Read Then
                    If par.IfNull(myReader5("tipo"), "E") = "E" Then
                        tipolotto.Value = "E"
                    Else
                        tipolotto.Value = "I"
                    End If
                End If
                myReader5.Close()

            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function


    Protected Sub ImgProcedi0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi0.Click
        Response.Redirect("SceltaServizio.aspx?IDV=" & idVoce.Value & "&IDP=" & idPianoF.Value)
    End Sub

    Protected Sub ImgNuovoLotto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNuovoLotto.Click

        CaricaLotti()
        ServiziAssociati()
    End Sub



    Protected Sub ImgEliminaLotto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEliminaLotto.Click
        If elimina.Value = "1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                'par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio where id_lotto=" & cmbLotti.SelectedItem.Value
                'par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from siscom_mi.lotti where id=" & cmbLotti.SelectedItem.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F85','" & par.PulisciStrSql(cmbLotti.SelectedItem.Text) & "'," & Session.Item("ID_STRUTTURA") & ")"
                par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")


                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaLotti()

            Catch ex As Exception

                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Protected Sub ImgModificaLotto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModificaLotto.Click
        CaricaLotti()
        If selezionato.Value <> "0" Then
            cmbLotti.Items.FindByValue(selezionato.Value).Selected = True
            ServiziAssociati()
        End If

    End Sub

    Protected Sub cmbLotti_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLotti.SelectedIndexChanged
        ServiziAssociati()
        selezionato.Value = cmbLotti.SelectedItem.Value
    End Sub
End Class
