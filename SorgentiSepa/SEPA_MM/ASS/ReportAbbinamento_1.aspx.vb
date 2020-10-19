Imports System.IO

Partial Class ASS_ReportAbbinamento_1
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreIDALL As String
    Dim SValoreG As String
    Dim s_Stringasql As String
    Dim SValoreOfferta As String
    Dim SDATAOfferta As String
    Dim contenuto As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            sValoreIDALL = Request.QueryString("IDALL")
            SValoreG = Request.QueryString("DATAS")
            SValoreOfferta = Request.QueryString("ABB")
            SDATAOfferta = Request.QueryString("DATAP")
            If IsNumeric(sValoreIDALL) Then
                Try
                    'lblPg.Text = "PG: " & sValorePG


                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\Accettazione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()


                    Label2.Text = "ESITO OFFERTA ALLOGGIO N° " & SValoreOfferta

                    contenuto = Replace(contenuto, "$Label2$", Label2.Text)


                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS DESCRIZIONEPIANO,GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE ALLOGGI.PIANO=TIPO_LIVELLO_PIANO.COD (+) AND t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod_GESTORE=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & sValoreIDALL
                    'par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS DESCRIZIONEPIANO,GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE ALLOGGI.PIANO=TIPO_LIVELLO_PIANO.COD (+) AND t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod_GESTORE=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & sValoreIDALL



                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        If par.IfNull(myReader("ID_PRATICA"), "0") > 500000 Then
                            Label5.Text = "BANDO CAMBI"
                            contenuto = Replace(contenuto, "$Label5$", Label5.Text)
                        End If
                        If par.IfNull(myReader("ID_PRATICA"), "0") > 8000000 Then
                            Label5.Text = "CAMBI EMERGENZA"
                            contenuto = Replace(contenuto, "$Label5$", Label5.Text)
                        End If
                        lblCodice.Text = par.IfNull(myReader("COD_ALLOGGIO"), "")
                        contenuto = Replace(contenuto, "$codice$", lblCodice.Text)

                        lblZona.Text = par.IfNull(myReader("zona"), "")
                        contenuto = Replace(contenuto, "$zona$", lblZona.Text)

                        lblLocali.Text = par.IfNull(myReader("num_locali"), "")
                        contenuto = Replace(contenuto, "$locali$", lblLocali.Text)


                        lblPiano.Text = par.IfNull(myReader("DESCRIZIONEPIANO"), "")
                        contenuto = Replace(contenuto, "$piano$", lblPiano.Text)

                        lblCondizione.Text = par.IfNull(myReader("condizione"), "")
                        contenuto = Replace(contenuto, "$condizione$", lblCondizione.Text)


                        lblSup.Text = par.IfNull(myReader("sup"), "")
                        contenuto = Replace(contenuto, "$sup$", lblSup.Text)

                        lblAlloggio.Text = par.IfNull(myReader("num_ALLOGGIO"), "")
                        contenuto = Replace(contenuto, "$alloggio$", lblAlloggio.Text)

                        lblScala.Text = par.IfNull(myReader("scala"), "")
                        contenuto = Replace(contenuto, "$scala$", lblScala.Text)


                        If par.IfNull(myReader("eqcanone"), "0") = "0" Then
                            lblCanone.Text = "NO"

                        Else
                            lblCanone.Text = "SI"
                        End If
                        contenuto = Replace(contenuto, "$canone$", lblCanone.Text)


                        If par.IfNull(myReader("ascensore"), "0") = "0" Then
                            lblAscensore.Text = "NO"
                        Else
                            lblAscensore.Text = "SI"
                        End If
                        contenuto = Replace(contenuto, "$ascensore$", lblAscensore.Text)

                        If par.IfNull(myReader("h_motorio"), "0") = "0" Then
                            lblMotorio.Text = "NO"
                        Else
                            lblMotorio.Text = "SI"
                        End If
                        contenuto = Replace(contenuto, "$motorio$", lblMotorio.Text)


                        lblComunicazione.Text = par.FormattaData(par.IfNull(myReader("data_comunicazione"), ""))
                        contenuto = Replace(contenuto, "$comunicazione$", lblComunicazione.Text)


                        lblDisponibile.Text = par.FormattaData(par.IfNull(myReader("data_disponibilita"), ""))
                        contenuto = Replace(contenuto, "$disponibilita$", lblDisponibile.Text)

                        lblProprieta.Text = par.IfNull(myReader("proprieta"), "")
                        contenuto = Replace(contenuto, "$proprieta$", lblProprieta.Text)

                        lblTipologia.Text = par.IfNull(myReader("tipoalloggio"), "")
                        contenuto = Replace(contenuto, "$tipologia$", lblTipologia.Text)


                        lblIndirizzo.Text = par.IfNull(myReader("tipoindirizzo"), "") & " " & par.IfNull(myReader("indirizzo"), "") & " " & par.IfNull(myReader("num_civico"), "")
                        contenuto = Replace(contenuto, "$indirizzo$", lblIndirizzo.Text)


                        lblStato.Text = par.IfNull(myReader("stato"), "")
                        contenuto = Replace(contenuto, "$stato$", lblStato.Text)


                        If par.IfNull(myReader("TIPO"), "") = "0" Then
                            lblGestore.Text = par.IfNull(myReader("DESCRIZIONE"), "") & " - " & par.IfNull(myReader("SEDE"), "") & " - " & par.IfNull(myReader("TELEFONO"), "") & " - ERP"
                        Else
                            lblGestore.Text = par.IfNull(myReader("DESCRIZIONE"), "") & " - " & par.IfNull(myReader("SEDE"), "") & " - " & par.IfNull(myReader("TELEFONO"), "") & " - EQ"
                        End If
                        contenuto = Replace(contenuto, "$gestore$", lblGestore.Text)

                        lblDataOfferta.Text = par.FormattaData(SDATAOfferta)
                        contenuto = Replace(contenuto, "$dataofferta$", lblDataOfferta.Text)


                        LBLNominativo.Text = Session.Item("NOMINATIVO")
                        contenuto = Replace(contenuto, "$nominativo$", LBLNominativo.Text)

                        Label1.Text = "" '"SI AVVERTE CHE LA RISPOSTA DOVRA' ESSERE COMUNICATA A QUESTO UFFICIO ENTRO IL GIORNO " & SValoreG & ", RESTITUENDO IL PRESENTE MODULO. LA MANCATA RISPOSTA VERRA' CONSIDERATA RINUNCIA ALLA PRESENTE OFFERTA."
                    End If

                    If Request.QueryString("RISP") = "0" Then
                        lblNonAccetta.Text = "X"
                        lblAccetta.Text = ""
                        lblMotivo.Text = Request.QueryString("MOT")

                        contenuto = Replace(contenuto, "$nonaccetta$", "X")
                        contenuto = Replace(contenuto, "$accetta$", "")
                        contenuto = Replace(contenuto, "$motivo$", lblMotivo.Text)

                    Else
                        lblNonAccetta.Text = ""
                        lblAccetta.Text = "X"
                        lblMotivo.Text = ""
                        contenuto = Replace(contenuto, "$nonaccetta$", "")
                        contenuto = Replace(contenuto, "$accetta$", "X")
                        contenuto = Replace(contenuto, "$motivo$", "")
                    End If
                    lblData.Text = Format(Now, "dd/MM/yyyy")
                    contenuto = Replace(contenuto, "$data$", lblData.Text)

                    LBLPG.Text = Request.QueryString("PG")
                    contenuto = Replace(contenuto, "$pg$", LBLPG.Text)


                    LBLNominativo.Text = Request.QueryString("NOM")
                    contenuto = Replace(contenuto, "$nominativo$", LBLNominativo.Text)

                    lblNote.Text = Session.Item("NOTEABBINAMENTO")
                    contenuto = Replace(contenuto, "$note$", lblNote.Text)

                    Session.Remove("NOTEABBINAMENTO")

                    Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\Contratti\StampeContratti\Ass_") & LBLPG.Text & ".html", False, System.Text.Encoding.GetEncoding("UTF-8"))
                    sr.WriteLine(contenuto)
                    sr.Close()


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

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

    End Sub
End Class
