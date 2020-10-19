
Partial Class RegistraUtente
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Dim lid_Operatore As Long

        If par.IfEmpty(txtUtente.Text, "") = "" Then
            lblErrore.Visible = True
            lblErrore.Text = "Nome Utente Errato!"
            Exit Sub
        End If

        If par.ControllaCF(txtCF.Text) = False Then
            lblErrore.Visible = True
            lblErrore.Text = "Codice Fiscale Errato!"
            Exit Sub
        End If

        If par.ControllaCFNomeCognome(txtCF.Text, txtCognome.Text, txtNome.Text) = False Then
            lblErrore.Visible = True
            lblErrore.Text = "Codice Fiscale Errato!"
            Exit Sub
        End If


        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select COUNT(ID) from OPERATORI where COD_FISCALE='" & UCase(txtCF.Text) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader(0) > 0 Then
                    myReader.Close()
                    par.OracleConn.Close()

                    lblErrore.Visible = True
                    lblErrore.Text = "Codice Fiscale gia inserito nei nostri archivi. Non è possibile procedere!"
                    Exit Sub
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "select COUNT(ID) from OPERATORI where upper(operatore)='" & UCase(txtUtente.Text) & "'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader(0) > 0 Then
                    myReader.Close()
                    par.OracleConn.Close()

                    lblErrore.Visible = True
                    lblErrore.Text = "Operatore gia inserito nei nostri archivi. Cambiare nome utente!"
                    Exit Sub
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "select * from OPERATORI where id=" & Session.Item("ID_OPERATORE")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                par.cmd.CommandText = "INSERT INTO OPERATORI (ID,PW,PW_DATA_INSERIMENTO,REVOCA,FL_DA_CONFERMARE,FL_ELIMINATO) VALUES (SEQ_OPERATORI.NEXTVAL,'" & par.Cripta("SEPA") & "','" & Format(Now, "yyyyMMdd") & "',0,'1','0')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SEQ_OPERATORI.CURRVAL FROM DUAL"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    lid_Operatore = myReader1(0)
                End If
                myReader1.Close()


                par.cmd.CommandText = "UPDATE OPERATORI SET " _
                                    & "OPERATORE='" & par.PulisciStrSql(txtUtente.Text) _
                                    & "',COGNOME='" & par.PulisciStrSql(txtCognome.Text) _
                                    & "',NOME='" & par.PulisciStrSql(txtNome.Text) _
                                    & "',COD_FISCALE='" & par.PulisciStrSql(txtCF.Text) _
                                    & "',COD_ANA='" _
                                    & "',campus='" _
                                    & "',ASS_ESTERNA='0" _
                                    & "',ALLOGGI='0" _
                                    & "',AUTOCOMPILAZIONE='0" _
                                    & "',SEPA='0" _
                                    & "',LIVELLO=-1" _
                                    & ",SEPA_WEB='0" _
                                    & "',ID_CAF=" & myReader("id_caf") _
                                    & ",MOD_ERP='" & myReader("mod_erp") _
                                    & "',MOD_CAMBI='" & myReader("mod_cambi") _
                                    & "',MOD_FSA='" & myReader("mod_fsa") _
                                    & "',MOD_AU='" & myReader("mod_au") _
                                    & "',MOD_AU_CONS='" & myReader("mod_au_cons") _
                                    & "',MOD_ABB='" & myReader("mod_abb") _
                                    & "',MOD_ABB_DEC='" & myReader("mod_abb_dec") _
                                    & "',MOD_CONS='" & myReader("mod_cons") _
                                    & "',MOD_PED='" & myReader("mod_ped") _
                                    & "',ANAGRAFE='" & myReader("mod_ped") _
                                    & "',pg='" & par.IfNull(myReader("pg"), "") _
                                    & "',mod_demanio='" & myReader("mod_demanio") _
                                    & "',mod_manutenzioni='" & myReader("mod_manutenzioni") _
                                    & "',mod_contratti='" & myReader("mod_contratti") _
                                    & "',mod_bollette='" & myReader("mod_bollette") _
                                    & "',MOD_PED2='" & myReader("mod_ped2") _
                                    & "',MOD_PED2_ESTERNA='" & myReader("mod_ped2_esterna") _
                                    & "',FL_RESPONSABILE_ENTE='0" _
                                    & "',DATA_PW='" _
                                    & "' where id=" & lid_Operatore

                par.cmd.ExecuteNonQuery()
            End If







            Response.Write("<script>location.replace('ConfermaRegUtente.aspx?OP=" & UCase(par.VaroleDaPassare(txtUtente.Text)) & "');</script>")
            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub ImgHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgHome.Click
        Response.Write("<script>location.replace('AreaPrivata.aspx');</script>")
    End Sub
End Class
