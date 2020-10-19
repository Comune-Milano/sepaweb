
Partial Class ARCHIVIO_SchedaEustorgio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("MOD_ARCHIVIO_IM") = "0" Then
            ImgSalva.Visible = False
            cmbCategoria.Enabled = False
            cmbGestore.Enabled = False
            txtFaldone.Enabled = False
            txtScatola.Enabled = False
        End If
        If Session.Item("MOD_ARCHIVIO_C") = "0" Then
            ImgElimina.Visible = False
        End If
        If Not IsPostBack Then
            IndiceContratto = Request.QueryString("id")
            IndiceScheda = Request.QueryString("SC")
            CaricaDati()
            CaricaAttributi()
        End If

        cmbGestore.Attributes.Add("onchange", "javascript:RicavaEustorgio();")
        cmbCategoria.Attributes.Add("onchange", "javascript:RicavaEustorgio();")
        txtScatola.Attributes.Add("onchange", "javascript:RicavaEustorgio();")
    End Sub

    Private Function CaricaAttributi()
        Dim CTRL As Control = Nothing


        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, TextBox).Text))
            End If

            If TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, DropDownList).SelectedItem.Text))
            End If
        Next
    End Function

    Private Sub CaricaDati()
        Try
            Dim Intestatario As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.RiempiDList(Me, par.OracleConn, "cmbGestore", "SELECT * FROM SISCOM_MI.tab_gestori_archivio ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Me, par.OracleConn, "cmbCategoria", "SELECT * FROM SISCOM_MI.TAB_CAT_EUSTORGIO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            If IndiceScheda <> "-1" Then
                par.cmd.CommandText = "select RAPPORTI_UTENZA_ARCHIVIO.* from siscom_mi.rapporti_utenza_archivio where id=" & IndiceScheda
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    txtFaldone.Text = par.IfNull(myReader("FALDONE"), "")
                    cmbGestore.Items.FindByValue(Mid(par.IfNull(myReader("COD_EUSTORGIO"), "AL"), 1, 2)).Selected = True
                    cmbCategoria.Items.FindByValue(Mid(par.IfNull(myReader("COD_EUSTORGIO"), "AL"), 4, 2)).Selected = True
                    txtScatola.Text = Mid(par.IfNull(myReader("COD_EUSTORGIO"), "AL"), 7, 9)
                    txtNote.Text = par.IfNull(myReader("NOTE"), "")
                    esiste.Value = "1"
                End If
                myReader.Close()
            Else
                esiste.Value = "0"
                ImgElimina.Visible = False
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblEustorgio.Text = RicavaEustorgio()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
            ImgSalva.Visible = False
            ImgElimina.Visible = False
        End Try
    End Sub

    Public Property IndiceScheda() As String
        Get
            If Not (ViewState("par_IndiceScheda") Is Nothing) Then
                Return CStr(ViewState("par_IndiceScheda"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceScheda") = value
        End Set

    End Property

    Public Property IndiceContratto() As String
        Get
            If Not (ViewState("par_IndiceContratto") Is Nothing) Then
                Return CStr(ViewState("par_IndiceContratto"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceContratto") = value
        End Set

    End Property

    Protected Sub ImgAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgAnnulla.Click
        Response.Write("<script>if(opener.document.getElementById('btnAggiorna')){opener.document.getElementById('btnAggiorna').click();};self.close();</script>")
    End Sub

    Private Function RicavaEustorgio() As String
        Dim Testo As String = ""

        Testo = cmbGestore.SelectedItem.Value & "-"
        Testo = Testo & cmbCategoria.SelectedItem.Value & "-"
        Testo = Testo & UCase(txtScatola.Text)

        RicavaEustorgio = Testo
    End Function

    Protected Sub ImgSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Dim messaggio As String = ""
        Dim Modificato As Boolean = False
        Dim Testo As String = ""

        'If IsNumeric(txtScatola.Text) = False Then
        '    messaggio = "Il campo SCATOLA deve essere numerico e massimo 9 cifre\n"
        'Else
        '    txtScatola.Text = Format(CLng(txtScatola.Text), "000000000")
        'End If
        If txtScatola.Text = "" Then
            messaggio = messaggio & "Il campo SCATOLA deve contenere minimo 1 carattere e max 9 caratteri\n"
        End If
        If Len(txtScatola.Text) > 9 Then
            messaggio = messaggio & "Il campo SCATOLA deve contenere max 9 caratteri\n"
        End If
        'If txtFaldone.Text = "" Then
        '    messaggio = messaggio & "Inserire un valore nel campo FALDONE\n"
        'End If

        lblEustorgio.Text = RicavaEustorgio()
        If Len(lblEustorgio.Text) < 7 Then
            messaggio = messaggio & "CODICE EUSTORGIO non valido"
        End If

        If messaggio <> "" Then
            Response.Write("<script>alert('" & messaggio & "');</script>")
            Exit Sub
        End If
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()

            If esiste.Value = "0" Then 'inserimento
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO (ID,ID_CONTRATTO,COD_EUSTORGIO,FALDONE,GESTORE,CATEGORIA,SCATOLA,NOTE) VALUES (SISCOM_MI.SEQ_RU_ARCHIVIO.NEXTVAL," & IndiceContratto & ",'" & par.PulisciStrSql(UCase(lblEustorgio.Text)) & "','" & par.PulisciStrSql(UCase(txtFaldone.Text)) & "','" & cmbGestore.SelectedItem.Value & "','" & cmbCategoria.SelectedItem.Value & "','" & par.PulisciStrSql(UCase(txtScatola.Text)) & "','" & Mid(par.PulisciStrSql(txtNote.Text), 1, 300) & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI_ARCHIVIO (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
               & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
               & "'F55','" & par.PulisciStrSql("VALORI INSERITI: COD. EUSTORGIO:" & UCase(lblEustorgio.Text) & " - FALDONE:" & UCase(txtFaldone.Text)) & "')"
                par.cmd.ExecuteNonQuery()

                CaricaAttributi()
                Response.Write("<script>alert('Operazione effettuata!');if(opener.document.getElementById('btnAggiorna')){opener.document.getElementById('btnAggiorna').click();};self.close();</script>")

            Else
                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO SET COD_EUSTORGIO='" & par.PulisciStrSql(UCase(lblEustorgio.Text)) & "',FALDONE='" & par.PulisciStrSql(UCase(txtFaldone.Text)) & "',GESTORE='" & cmbGestore.SelectedItem.Value & "',CATEGORIA='" & cmbCategoria.SelectedItem.Value & "',SCATOLA='" & par.PulisciStrSql(UCase(txtScatola.Text)) & "',NOTE='" & Mid(par.PulisciStrSql(txtNote.Text), 1, 300) & "' WHERE ID=" & IndiceScheda
                par.cmd.ExecuteNonQuery()

                If txtFaldone.Text.ToUpper <> txtFaldone.Attributes("CORRENTE").ToUpper.ToString Then
                    Modificato = True
                    Testo = "Faldone da:" & txtFaldone.Attributes("CORRENTE").ToUpper.ToString & " a " & txtFaldone.Text.ToUpper
                End If
                If txtScatola.Text.ToUpper <> txtScatola.Attributes("CORRENTE").ToUpper.ToString Then
                    Modificato = True
                    Testo = Testo & " - Scatola da:" & txtScatola.Attributes("CORRENTE").ToUpper.ToString & " a " & txtScatola.Text.ToUpper
                End If
                If cmbGestore.SelectedItem.Text.ToUpper <> cmbGestore.Attributes("CORRENTE").ToUpper.ToString Then
                    Modificato = True
                    Testo = Testo & " - Gestore da:" & cmbGestore.Attributes("CORRENTE").ToUpper.ToString & " a " & cmbGestore.SelectedItem.Text.ToUpper
                End If
                If cmbCategoria.SelectedItem.Text.ToUpper <> cmbCategoria.Attributes("CORRENTE").ToUpper.ToString Then
                    Modificato = True
                    Testo = Testo & " - Categoria da:" & cmbCategoria.Attributes("CORRENTE").ToUpper.ToString & " a " & cmbCategoria.SelectedItem.Text.ToUpper
                End If
                If txtNote.Text.ToUpper <> txtNote.Attributes("CORRENTE").ToUpper.ToString Then
                    Modificato = True
                    Testo = Testo & " - NOTE da:" & txtNote.Attributes("CORRENTE").ToUpper.ToString & " a " & txtNote.Text.ToUpper
                End If
                If Modificato = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI_ARCHIVIO (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                  & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                  & "'F02','" & par.PulisciStrSql("VALORI MODIFICATI: " & Testo) & "')"
                    par.cmd.ExecuteNonQuery()

                    CaricaAttributi()
                End If

                esiste.Value = "1" 'modifica
                Response.Write("<script>alert('Operazione effettuata!');</script>")
            End If
            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub ImgElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgElimina.Click
        If cancella.Value = "1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO WHERE ID_CONTRATTO=" & IndiceContratto
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI_ARCHIVIO (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
               & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
               & "'F56','" & par.PulisciStrSql("VALORI PRESENTI: COD. EUSTORGIO:" & lblEustorgio.Text & " - FALDONE:" & txtFaldone.Text) & "')"
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                esiste.Value = "0"
                Response.Write("<script>alert('Operazione effettuata!');if(opener.document.getElementById('btnAggiorna')){opener.document.getElementById('btnAggiorna').click();};self.close();</script>")
            Catch ex As Exception
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub
End Class
