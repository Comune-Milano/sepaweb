' TAB GENERALE DEL DETTAGLIO DELLA MOROSITA' DELL'INQUILINO

Partial Class Tab_MorositaInquilino_Gen
    Inherits UserControlSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSolaLettura()
            End If

        End If
        If Session.Item("MOD_MOROSITA_ANN") <> "1" Then
            Me.btnAnnulloGlobal.Visible = False
            Me.btnAnnulloAler.Visible = False
        End If
    End Sub

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next

        Catch ex As Exception

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub


    Private Sub AnnullaMav(ByVal tipo As String)
        Dim idMor As String = ""
        If tipo = "LEFT" Then
            idMor = "idMorLeft"
        ElseIf tipo = "RIGHT" Then
            idMor = "idMorRight"

        End If
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

        End If

        Try
            If CType(Me.Page.FindControl(idMor), HiddenField).Value > 0 Then
                Dim idMorLettDaAnnullare As String
                idMorLettDaAnnullare = CType(Me.Page.FindControl(idMor), HiddenField).Value
                Dim lettoreStato As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "SELECT * FROM siscom_mi.MOROSITA_LETTERE WHERE id = " & idMorLettDaAnnullare & " AND cod_stato='M00'"
                lettoreStato = par.cmd.ExecuteReader
                If lettoreStato.HasRows Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE FL_ANNULLATA=0 AND id_rateizzazione IS NULL AND NVL(importo_pagato,0) =0 AND rif_bollettino IN (SELECT bollettino FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID=" & idMorLettDaAnnullare & ")"
                    Dim lettoreBolletta As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreBolletta.Read Then
                        '1)ANNULO MAV MOROSITA
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA=1,data_annullo='" & Format(Now, "yyyyMMdd") & "' WHERE id_tipo=4 AND ID=" & lettoreBolletta("ID")
                        par.cmd.ExecuteNonQuery()
                        '2)RIPRISTINO TUTTE le BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO=NULL " _
                            & "WHERE ID_BOLLETTA IN (SELECT ID FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_BOLLETTA_RIC = " & lettoreBolletta("ID") & " AND ID_RATEIZZAZIONE IS NULL ) " _
                            & "AND nvl(IMPORTO_RICLASSIFICATO,0)<>0"
                        par.cmd.ExecuteNonQuery()
                        '2bis)RIPRISTINO TUTTE le BOLLETTE interessate della MOROSITA (vIdMorosita)
                        par.cmd.CommandText = "UPDATE  SISCOM_MI.BOL_BOLLETTE  SET ID_BOLLETTA_RIC=NULL, ID_MOROSITA=NULL WHERE ID_BOLLETTA_RIC = " & lettoreBolletta("ID") & "  AND FL_ANNULLATA=0 AND ID_RATEIZZAZIONE IS NULL"
                        par.cmd.ExecuteNonQuery()
                        '3)MODIFICO LO STATO DELLE MOROSITA LETTERE
                        par.cmd.CommandText = "UPDATE  SISCOM_MI.MOROSITA_LETTERE SET COD_STATO='M98' WHERE ID IN (" & idMorLettDaAnnullare & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.MOROSITA_EVENTI (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL," & idMorLettDaAnnullare & " ," & Session.Item("id_operatore") & ",TO_CHAR(SYSDATE,'yyyyMMddHH24miss'),'M98','Annullata la morosita')"
                        par.cmd.ExecuteNonQuery()

                        Response.Write(<script>alert('Operazione Completata!');</script>)

                        par.myTrans.Commit()

                        CType(Me.Page, Object).VisualizzaDati()
                    Else
                        Response.Write(<script>alert('Impossibile Annullare!');</script>)
                        par.myTrans.Rollback()
                    End If
                    lettoreBolletta.Close()
                Else
                    Response.Write(<script>alert('Impossibile Annullare!');</script>)
                    par.myTrans.Rollback()

                End If
                lettoreStato.Close()

            Else
                Response.Write(<script>alert('Impossibile Annullare!');</script>)
                par.myTrans.Rollback()

            End If
        Catch ex As Exception
                        par.myTrans.Rollback()


        End Try




    End Sub


    Protected Sub btnAnnulloGlobal_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulloGlobal.Click
        If CType(Me.Page.FindControl("Confirm"), HiddenField).Value = 1 Then
            AnnullaMav("LEFT")
            CType(Me.Page.FindControl("Confirm"), HiddenField).Value = 0
        End If

    End Sub

    Protected Sub btnAnnulloAler_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulloAler.Click
        If CType(Me.Page.FindControl("Confirm"), HiddenField).Value = 1 Then
            AnnullaMav("RIGHT")
            CType(Me.Page.FindControl("Confirm"), HiddenField).Value = 0
        End If

    End Sub
End Class
