
Partial Class Rinnovo
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            lblData.Text = Format(Now, "dd/MM/yyyy")
            txtMotivo.Text = "INTEGRAZIONE DA ENTE ESTERNO"
            sValoreID = Request.QueryString("ID")
            Riempi()
            CHVERIFICA.Attributes.Add("onclick", "javascript:cambia();")
            chNoAssegnazione.Attributes.Add("onclick", "javascript:inAssegn();")
        End If
    End Sub

    Private Function Riempi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Function
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select anno_isee from bandi where stato=1 order by id desc"
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader11.Read Then
                AnnoIsee = myReader11("anno_isee")
            End If
            myReader11.Close()
            Label8.Text = annoisee


            par.cmd.CommandText = "select DOMANDE_BANDO.ID_TIPO_CONTENZIOSO,DOMANDE_BANDO.ID_DICHIARAZIONE,domande_bando.id_stato,domande_bando.pg,domande_bando.isbarc_r,comp_nucleo.cognome,comp_nucleo.NOME,domande_bando.id_dichiarazione from domande_bando,comp_nucleo where domande_bando.id=" & sValoreID & " and domande_bando.progr_componente=comp_nucleo.progr and comp_nucleo.id_dichiarazione=domande_bando.id_dichiarazione"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                sValoreST = myReader("id_stato")
                sValoreDI = myReader("ID_DICHIARAZIONE")
                If myReader("id_stato") = "4" Or myReader("id_stato") = "8" Or myReader("id_stato") = "2" Then
                    Label1.Text = "INTEGRAZIONE Domanda N. " & myReader("pg") & " Intestata a : " & myReader("cognome") & " " & myReader("nome")
                    If myReader("id_stato") = "2" Then
                        Image2.Visible = True
                        Label5.Visible = True
                        If myReader("id_tipo_contenzioso") = "138" Or myReader("id_tipo_contenzioso") = "140" Then
                            Label5.Text = "DOMANDA CON ISTANZA DI DEROGA IN CORSO!"
                            sValoreDE = "1"
                        End If
                        If myReader("id_tipo_contenzioso") = "153" Then
                            Label5.Text = "DOMANDA CON RICORSO PER DEROGA RESP. IN CORSO!"
                            sValoreDE = "1"
                        End If
                    Else
                        sValoreDE = ""
                    End If
                Else
                    par.cmd.CommandText = "select COD_EVENTO FROM EVENTI_BANDI WHERE Id_DOMANDA=" & sValoreID & " ORDER BY DATA_ORA DESC"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        If myReader1("COD_EVENTO") = "F140" Then
                            btnSalva.Visible = False
                            btnDichiarazione.Visible = True
                            btnDomanda.Visible = True
                            txtMotivo.Enabled = False
                            Session.Item("CONFERMATO") = "1"
                        Else
                            Label1.Text = "Stato della domanda: " & myReader("id_stato") & " E' possibile integrare solo domande con stato 4 (Respinte) o 8 (In Graduatoria)."
                            btnSalva.Visible = False
                            btnDichiarazione.Visible = False
                            btnDomanda.Visible = False
                        End If
                    End If
                    myReader1.Close()
                End If
            End If

            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("CONFERMATO", "0")
            Session.Add("LAVORAZIONE", "1")

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Function

    Public Property sValoreST() As String
        Get
            If Not (ViewState("par_sValoreST") Is Nothing) Then
                Return CStr(ViewState("par_sValoreST"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sValoreST") = value
        End Set

    End Property

    Public Property AnnoIsee() As String
        Get
            If Not (ViewState("par_AnnoIsee") Is Nothing) Then
                Return CStr(ViewState("par_AnnoIsee"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_AnnoIsee") = value
        End Set

    End Property

    Public Property sValoreDI() As String
        Get
            If Not (ViewState("par_sValoreDI") Is Nothing) Then
                Return CStr(ViewState("par_sValoreDI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sValoreDI") = value
        End Set

    End Property


    Public Property sValoreDE() As String
        Get
            If Not (ViewState("par_sValoreDE") Is Nothing) Then
                Return CStr(ViewState("par_sValoreDE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sValoreDE") = value
        End Set

    End Property

    Public Property sValoreID() As String
        Get
            If Not (ViewState("par_sValoreID") Is Nothing) Then
                Return CStr(ViewState("par_sValoreID"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sValoreID") = value
        End Set

    End Property



    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If Session.Item("CONFERMATO") = "1" Then
            Response.Write("<script>alert('Attenzione...Devi necessariamente visualizzare ed elaborare la domanda!');</script>")
        Else
            Session.Item("LAVORAZIONE") = "0"
            Response.Write("<script>document.location.href=""RicercaRinnovi.aspx""</script>")
        End If

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If txtMotivo.Text = "" Then
            Response.Write("<script>alert('Attenzione...valorizzare il campo Motivazione!');</script>")
        Else
            Try
                If t1.Text = "0" Then Exit Sub
                Label6.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label7.Visible = True
                Label7.Text = "ATTENZIONE, modificare la dichiarazione aggiornando i redditi al " & AnnoIsee
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "INSERT INTO RINNOVI (ID,ID_PRATICA,TIPO_FILE,IMG,DATA,RAPPRESENTANTE,MOTIVAZIONE,PROTOCOLLO) VALUES (SEQ_RINNOVI.NEXTVAL," & sValoreID & ",'TIF',EMPTY_BLOB(),'" & par.AggiustaData(lblData.Text) & "','','" & par.PulisciStrSql(txtMotivo.Text) & "','')"
                par.cmd.ExecuteNonQuery()

                If Image2.Visible = True Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_RINNOVO='1',ISBAR=0,ISBARC=0,ISBARC_R=0,DISAGIO_F=0,DISAGIO_E=0,DISAGIO_A=0,REDDITO_ISEE=0,ID_STATO='2',fl_completa='1',FL_ESAMINATA='0',FL_ISTRUTTORIA_COMPLETA='0' WHERE ID=" & sValoreID
                Else
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_RINNOVO='1',ISBAR=0,ISBARC=0,ISBARC_R=0,DISAGIO_F=0,DISAGIO_E=0,DISAGIO_A=0,REDDITO_ISEE=0,ID_STATO='2',fl_completa='1',FL_ESAMINATA='0',FL_ISTRUTTORIA_COMPLETA='0',ID_VECCHIO_STATO='" & sValoreST & "' WHERE ID=" & sValoreID
                End If
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update dichiarazioni set anno_sit_economica=" & AnnoIsee & " where id=" & sValoreDI
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    & "VALUES (" & sValoreID & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sValoreST _
                    & "','F140','','I')"
                par.cmd.ExecuteNonQuery()

                btnSalva.Visible = False
                VisDichiarazione.Visible = False
                VisDomanda.Visible = False
                btnDichiarazione.Visible = True
                btnDomanda.Visible = True
                txtMotivo.Enabled = False
                Image1.Visible = True

                Session.Item("CONFERMATO") = "1"
                'If (Session.Item("CAAF") = "ALERMI" And sValoreST <> "4") Or Session.Item("LIVELLO") = "1" Then
                '    CHVERIFICA.Enabled = True
                'End If
                'Session.Add("MOD_ERP_REQUISITI", myReader("MOD_ERP_REQUISITI"))
                If Session.Item("MOD_ERP_REQUISITI") = "1" And sValoreST <> "4" Then
                    chNoAssegnazione.Enabled = True
                    CHVERIFICA.Enabled = True
                    CHVERIFICA.Checked = True
                    TextBox1.Text = "1"
                End If

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = EX1.Message
            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub

    Private Sub chNoAssegnazione_CheckedChanged(sender As Object, e As EventArgs) Handles chNoAssegnazione.CheckedChanged
        Session.Item("CONFERMATO") = "1"
    End Sub
End Class
