
Partial Class Public_VerificaPos
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT count(data) from stat_web"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Label4.Text = "Sei il visitatore n° " & par.IfNull(myReader(0), "")
            End If
            myReader.Close()
            par.OracleConn.Close()
            'par.OracleConn.Dispose()
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            'par.OracleConn.Dispose()
        Catch ex As Exception
            par.OracleConn.Close()
            'par.OracleConn.Dispose()
        End Try
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click

        Dim sss As String = ""

        If txtCF.Text <> "" Or txtPG.Text <> "" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "insert into stat_web (data,tipo_op) values ('" & Format(Now, "yyyyMMdd") & "','0')"
                par.cmd.ExecuteNonQuery()

                If txtPG.Text <> "" Then
                    sss = "DOMANDE_BANDO.PG='" & par.PulisciStrSql(txtPG.Text) & "'"
                End If
                If txtCF.Text <> "" Then
                    If txtPG.Text <> "" Then
                        sss = sss & "AND COMP_NUCLEO.COD_FISCALE='" & UCase(par.PulisciStrSql(txtCF.Text)) & "'"
                    Else
                        sss = "COMP_NUCLEO.COD_FISCALE='" & UCase(par.PulisciStrSql(txtCF.Text)) & "'"
                    End If
                End If
                par.cmd.CommandText = "SELECT bandi.stato,DOMANDE_BANDO.ISBARC_R,DOMANDE_BANDO.pg,DOMANDE_BANDO.data_pg,DOMANDE_BANDO.ID,comp_nucleo.cognome,comp_nucleo.nome,comp_nucleo.data_nascita FROM DOMANDE_BANDO,COMP_NUCLEO,bandi WHERE " & sss & " AND domande_bando.id_bando=bandi.id and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE "
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lblPosizione.Visible = True
                    lblData.Visible = True
                    lblNome.Visible = True
                    LBLPG.Visible = True
                    LBLPG.Text = "Protocollo:" & par.IfNull(myReader("PG"), "") & " Data:" & par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                    lblNome.Text = par.IfNull(myReader("Cognome"), "") & " " & par.IfNull(myReader("nome"), "")
                    lblData.Text = "Data di Nascita: " & par.FormattaData(par.IfNull(myReader("data_nascita"), ""))
                    par.cmd.CommandText = "SELECT posizione,ISBARC_R FROM bandi_graduatoria_def WHERE id_domanda=" & par.IfNull(myReader("ID"), "-1")
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lblPosizione.Text = "Posizione : " & par.IfNull(myReader1("posizione"), "")
                        LBLPG.Text = LBLPG.Text & " ISBARC/R:" & par.Tronca(par.IfNull(myReader1("ISBARC_R"), "0"))
                    Else
                        If par.IfNull(myReader("stato"), "") = "1" Then
                            lblPosizione.Text = "IN ATTESA DI COLLOCAZIONE IN GRAD."
                        Else
                            lblPosizione.Text = "QUESTA DOMANDA NON E' IN GRADUATORIA!"
                        End If
                        LBLPG.Text = LBLPG.Text & " ISBARC/R:" & par.Tronca(par.IfNull(myReader("ISBARC_R"), "0"))
                    End If
                    myReader1.Close()
                Else
                    txtCF.Visible = True
                    txtPG.Visible = True
                    Label2.Visible = True
                    lblPosizione.Visible = False
                    lblData.Visible = False
                    lblNome.Visible = False
                    LBLPG.Visible = False
                    btnCerca.Text = "Cerca"
                    txtCF.Text = ""
                    txtPG.Text = ""
                    Response.Write("<script>alert('Domanda non presente in archivio!');</script>")
                End If
                myReader.Close()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        Else
            Response.Write("<script>alert('Inserire Protocollo e Codice Fiscale!');</script>")
        End If
        'End If
    End Sub


End Class
