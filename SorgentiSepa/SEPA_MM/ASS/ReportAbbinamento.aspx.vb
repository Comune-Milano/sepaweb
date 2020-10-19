
Partial Class ASS_ReportAbbinamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreIDALL As String
    Dim SValoreG As String = ""
    Dim s_Stringasql As String
    Dim SValoreOfferta As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            sValoreIDALL = Request.QueryString("IDALL")
            SValoreG = Request.QueryString("DATAS")
            SValoreOfferta = Request.QueryString("ABB")
            If IsNumeric(sValoreIDALL) Then
                Try
                    'lblPg.Text = "PG: " & sValorePG
                    Label2.Text = "OFFERTA ALLOGGIO N° " & SValoreOfferta
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    If SValoreG = "" Then
                        par.cmd.CommandText = "select * from DOMANDE_OFFERTE_SCAD where id=" & SValoreOfferta
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read() Then
                            SValoreG = par.FormattaData(par.IfNull(myReader1("data_scadenza"), ""))
                        End If
                        myReader1.Close()
                    End If

                    par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS DESCRIZIONEPIANO,GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE ALLOGGI.PIANO=TIPO_LIVELLO_PIANO.COD (+) AND t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod_GESTORE=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & sValoreIDALL



                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        If par.IfNull(myReader("ID_PRATICA"), "0") > 500000 Then
                            Label5.Text = "BANDO CAMBI"
                        End If
                        If par.IfNull(myReader("ID_PRATICA"), "0") > 8000000 Then
                            Label5.Text = "CAMBI EMERGENZA"
                        End If
                        '

                        lblCodice.Text = par.IfNull(myReader("COD_ALLOGGIO"), "")
                        lblZona.Text = par.IfNull(myReader("zona"), "")
                        lblLocali.Text = par.IfNull(myReader("num_locali"), "")


                        lblPiano.Text = par.IfNull(myReader("DESCRIZIONEPIANO"), "")


                        lblCondizione.Text = par.IfNull(myReader("condizione"), "")
                        lblSup.Text = par.IfNull(myReader("sup"), "")
                        lblAlloggio.Text = par.IfNull(myReader("num_ALLOGGIO"), "")
                        lblScala.Text = par.IfNull(myReader("scala"), "")

                        If par.IfNull(myReader("eqcanone"), "0") = "0" Then
                            lblCanone.Text = "NO"
                        Else
                            lblCanone.Text = "SI"
                        End If

                        If par.IfNull(myReader("ascensore"), "0") = "0" Then
                            lblAscensore.Text = "NO"
                        Else
                            lblAscensore.Text = "SI"
                        End If

                        If par.IfNull(myReader("h_motorio"), "0") = "0" Then
                            lblMotorio.Text = "NO"
                        Else
                            lblMotorio.Text = "SI"
                        End If

                        lblComunicazione.Text = par.FormattaData(par.IfNull(myReader("data_comunicazione"), ""))
                        lblDisponibile.Text = par.FormattaData(par.IfNull(myReader("data_disponibilita"), ""))

                        lblProprieta.Text = par.IfNull(myReader("proprieta"), "")
                        lblTipologia.Text = par.IfNull(myReader("tipoalloggio"), "")
                        lblIndirizzo.Text = par.IfNull(myReader("tipoindirizzo"), "") & " " & par.IfNull(myReader("indirizzo"), "") & " " & par.IfNull(myReader("num_civico"), "")
                        lblStato.Text = par.IfNull(myReader("stato"), "")
                        If par.IfNull(myReader("TIPO"), "") = "0" Then
                            lblGestore.Text = par.IfNull(myReader("DESCRIZIONE"), "") & " - " & par.IfNull(myReader("SEDE"), "") & " - " & par.IfNull(myReader("TELEFONO"), "") & " - ERP"
                        Else
                            lblGestore.Text = par.IfNull(myReader("DESCRIZIONE"), "") & " - " & par.IfNull(myReader("SEDE"), "") & " - " & par.IfNull(myReader("TELEFONO"), "") & " - EQ"
                        End If

                        lblDataOfferta.Text = Format(Now, "dd/MM/yyyy")
                        LBLNominativo.Text = Session.Item("NOMINATIVO")

                        Label1.Text = "SI AVVERTE CHE LA RISPOSTA DOVRA' ESSERE COMUNICATA A QUESTO UFFICIO ENTRO IL GIORNO "
                        Label3.Text = SValoreG
                        Label4.Text = "RESTITUENDO IL PRESENTE MODULO. LA MANCATA RISPOSTA VERRA' CONSIDERATA RINUNCIA ALLA PRESENTE OFFERTA."
                    End If
                    myReader.Close()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                Catch ex As Exception
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                End Try
            End If
        End If
    End Sub
End Class
