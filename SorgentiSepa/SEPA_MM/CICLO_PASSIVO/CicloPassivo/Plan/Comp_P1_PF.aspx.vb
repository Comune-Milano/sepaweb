
Partial Class Contabilita_CicloPassivo_Plan_Comp_P1_PF
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idPianoF.Value = Request.QueryString("ID")
            CaricaStato()
            CaricaVoci()
            ' Verifica()



        End If
    End Sub

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                per.Value = Label1.Text
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
                If par.IfNull(myReader5("stato"), "") <> "CARICAMENTO IMPORTI" Then
                    Dim CTRL As Control
                    For Each CTRL In Me.form1.Controls
                        If TypeOf CTRL Is TextBox Then
                            DirectCast(CTRL, TextBox).Enabled = False
                        ElseIf TypeOf CTRL Is DropDownList Then
                            DirectCast(CTRL, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL Is CheckBox Then
                            DirectCast(CTRL, CheckBox).Enabled = False
                        ElseIf TypeOf CTRL Is ImageButton Then
                            DirectCast(CTRL, ImageButton).Visible = False
                        ElseIf TypeOf CTRL Is Web.UI.WebControls.Image Then
                            If DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEsci" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgAnnotazioni0" Then
                                DirectCast(CTRL, Web.UI.WebControls.Image).Visible = False
                            End If
                        End If
                    Next
                End If

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

    Function CaricaVoci()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            
            par.RiempiDList(Me, par.OracleConn, "cmbVoci", "select id,codice||'-'||descrizione as descr from siscom_mi.pf_voci,SISCOM_MI.PF_VOCI_STRUTTURA WHERE PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE AND PF_VOCI_STRUTTURA.ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") & " AND pf_voci.id_piano_finanziario=" & idPianoF.Value & " and id_voce_madre is null and PF_VOCI.id in (select id_voce from siscom_mi.pf_voci_operatori where id_voce=pf_voci.id AND ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ")", "DESCR", "ID")



            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("BP_COMPILAZIONE_L"), "1") = "1" Then
                    Dim CTRL As Control
                    For Each CTRL In Me.form1.Controls
                        If TypeOf CTRL Is TextBox Then
                            DirectCast(CTRL, TextBox).Enabled = False
                        ElseIf TypeOf CTRL Is DropDownList Then
                            DirectCast(CTRL, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL Is CheckBox Then
                            DirectCast(CTRL, CheckBox).Enabled = False
                        ElseIf TypeOf CTRL Is ImageButton Then
                            DirectCast(CTRL, ImageButton).Visible = False
                        ElseIf TypeOf CTRL Is Web.UI.WebControls.Image Then
                            If DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEsci" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgAnnotazioni0" Then
                                DirectCast(CTRL, Web.UI.WebControls.Image).Visible = False
                            End If
                        End If
                    Next

                End If
            End If
            myReader1.Close()


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


    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click

        If cmbVoci.SelectedIndex = -1 Then
            Response.Write("<script>alert('Specificare una voce!');</script>")
            Exit Sub
        End If

        Response.Redirect("SottoVoci.aspx?IDV=" & cmbVoci.SelectedItem.Value & "&IDP=" & idPianoF.Value)


    End Sub


End Class
