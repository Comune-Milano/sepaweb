
Partial Class ASS_Contratto
    Inherits PageSetIdMode
    Dim par As New CM.Global()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If IsPostBack = False Then
            lIdDomanda = CLng(Request.QueryString("ID"))
            lIdOfferta = CLng(Request.QueryString("OF"))
            lblScad.Text = "Offerto il: " & Request.QueryString("SC") & " Accettato il:" & Request.QueryString("ACC")
            If lIdDomanda < 500000 Then
                VisualizzaDomanda()
            Else
                VisualizzaDomandaCambi()
            End If

            If UCase(lblProprieta.Text) = "COMUNE" Then
                ImgSalva.Visible = False
                TXTCONTRATTO.Enabled = False
                TXTDATA.Enabled = False
                TXTDATADEC.Enabled = False

                Response.Write("<script>alert('Attenzione...questa funzione è utilizzabile solo per alloggi di proprietà non Comunale!');</script>")
            End If
        End If
        TXTDATA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TXTDATADEC.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub
    Public Property lIdOfferta() As Long
        Get
            If Not (ViewState("par_lIdOfferta") Is Nothing) Then
                Return CLng(ViewState("par_lIdOfferta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdOfferta") = value
        End Set

    End Property

    Public Property lIdDomanda() As Long
        Get
            If Not (ViewState("par_lIdDomanda") Is Nothing) Then
                Return CLng(ViewState("par_lIdDomanda"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDomanda") = value
        End Set

    End Property

    Private Function VisualizzaDomandaCambi()
        Dim scriptblock As String


        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            lblOfferta.Text = "Offerta N° " & lIdOfferta
            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_cambi WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                myReader.Close()
                par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_cambi.DATA_PG,DOMANDE_BANDO_cambi.id_bando,DOMANDE_BANDO_cambi.PG,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,DOMANDE_BANDO_cambi.tipo_pratica FROM DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi WHERE DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=COMP_NUCLEO_cambi.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_cambi.PROGR=DOMANDE_BANDO_cambi.PROGR_COMPONENTE AND DOMANDE_BANDO_cambi.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    lblIsbarcr.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_cambi.ID) FROM COMP_NUCLEO_cambi,dichiarazioni_cambi,domande_bando_cambi WHERE DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND DOMANDE_BANDO_cambi.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT id from alloggi where id_pratica=" & lIdDomanda & " and prenotato='1'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    lblIdAll.Text = par.IfNull(myReader2("id"), "-1")
                    par.cmd.CommandText = "SELECT GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & par.IfNull(myReader2("id"), "-1")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader3.Read() Then
                        lblCodice.Text = par.IfNull(myReader3("COD_ALLOGGIO"), "")
                        lblZona.Text = par.IfNull(myReader3("zona"), "")
                        lblProprieta.Text = par.IfNull(myReader3("proprieta"), "")
                        lblStato.Text = par.IfNull(myReader3("stato"), "")
                        If par.IfNull(myReader3("TIPO"), "") = "0" Then
                            lblGestore.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - ERP"
                        Else
                            lblGestore.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - EQ"
                        End If
                    End If
                    myReader3.Close()
                Else
                    ImgSalva.Enabled = False
                End If

                myReader2.Close()

                par.cmd.CommandText = "select * from SISCOM_MI.UNITA_ASSEGNATE WHERE N_OFFERTA=" & lIdOfferta & " AND ID_DOMANDA=" & lIdDomanda
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    If par.IfNull(myReader2("PROVVEDIMENTO"), "") <> "" Then
                        lblProvvedimento.Text = par.IfNull(myReader2("PROVVEDIMENTO"), "") & " del " & par.FormattaData(par.IfNull(myReader2("DATA_PROVVEDIMENTO"), ""))
                        HiddenField2.Value = lblProvvedimento.Text
                    End If
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & lIdDomanda & " and ultimo='1'"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    lblRelazione.Text = myReader2("id")
                Else
                    lblRelazione.Text = "1"
                End If
                myReader2.Close()

                myReader.Close()



                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            Else
                myReader.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.ToString & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try
    End Function


    Private Function VisualizzaDomanda()
        Dim scriptblock As String


        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            lblOfferta.Text = "Offerta N° " & lIdOfferta
            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                myReader.Close()
                par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO.DATA_PG,DOMANDE_BANDO.id_bando,DOMANDE_BANDO.PG,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.tipo_pratica FROM DOMANDE_BANDO,COMP_NUCLEO WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND DOMANDE_BANDO.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    lblIsbarcr.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT id from alloggi where id_pratica=" & lIdDomanda & " and prenotato='1'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    lblIdAll.Text = par.IfNull(myReader2("id"), "-1")
                    par.cmd.CommandText = "SELECT GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod_GESTORE=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & par.IfNull(myReader2("id"), "-1")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader3.Read() Then
                        lblCodice.Text = par.IfNull(myReader3("COD_ALLOGGIO"), "")
                        lblZona.Text = par.IfNull(myReader3("zona"), "")
                        lblProprieta.Text = par.IfNull(myReader3("proprieta"), "")
                        lblStato.Text = par.IfNull(myReader3("stato"), "")
                        If par.IfNull(myReader3("TIPO"), "") = "0" Then
                            lblGestore.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - ERP"
                        Else
                            lblGestore.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - EQ"
                        End If
                    End If
                    myReader3.Close()
                Else
                    ImgSalva.Enabled = False
                End If

                myReader2.Close()

                par.cmd.CommandText = "select * from SISCOM_MI.UNITA_ASSEGNATE WHERE N_OFFERTA=" & lIdOfferta & " AND ID_DOMANDA=" & lIdDomanda
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    If par.IfNull(myReader2("PROVVEDIMENTO"), "") <> "" Then
                        lblProvvedimento.Text = par.IfNull(myReader2("PROVVEDIMENTO"), "") & " del " & par.FormattaData(par.IfNull(myReader2("DATA_PROVVEDIMENTO"), ""))
                        HiddenField2.Value = lblProvvedimento.Text
                    End If
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & lIdDomanda & " and ultimo='1'"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    lblRelazione.Text = myReader2("id")
                Else
                    lblRelazione.Text = "1"
                End If
                myReader2.Close()

                myReader.Close()



                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            Else
                myReader.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.ToString & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try
    End Function


    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Dim scriptblock As String

        If HiddenField1.Value = "1" Then
            If lblProvvedimento.Text = "NON ANCORA INSERITO" Then
                Response.Write("<script>alert('Provvedimento di Assegnazione non ancora inserito nel sistema!')</script>")
                Exit Sub
            End If

            If Len(TXTDATA.Text) <> 10 Or IsDate(TXTDATA.Text) = False Then
                Response.Write("<script>alert('Data contratto non valida!')</script>")
                Exit Sub
            End If
            If Len(TXTDATADEC.Text) <> 10 Or IsDate(TXTDATADEC.Text) = False Then
                Response.Write("<script>alert('Data decorrenza non valida!')</script>")
                Exit Sub
            End If
            If TXTCONTRATTO.Text <> "" Then
                Try

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                    End If

                    If lIdDomanda < 500000 Then
                        par.cmd.CommandText = "UPDATE domande_bando SET ID_STATO='10',CONTRATTO_NUM='" & TXTCONTRATTO.Text & "',CONTRATTO_DATA='" & par.AggiustaData(TXTDATA.Text) & "',CONTRATTO_DATA_DEC='" & par.AggiustaData(TXTDATADEC.Text) & "' WHERE ID=" & lIdDomanda
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                            & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,24,'" & lblPG.Text & "','" & LblDataPG.Text & "',10," & lblTipoPratica.Text & ",10)"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                            & "','F60','','I')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_ASSEGNATE SET GENERATO_CONTRATTO=1 WHERE ID_DOMANDA=" & lIdDomanda & " AND N_OFFERTA=" & lIdOfferta & " AND PROVENIENZA='G'"
                        par.cmd.ExecuteNonQuery()

                        ImgSalva.Enabled = False

                        par.myTrans.Commit()
                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                        Session.Item("LAVORAZIONE") = "0"
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Contratto inserito!');document.location.href='pagina_home.aspx';" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                        End If
                    Else
                        par.cmd.CommandText = "UPDATE domande_bando_cambi SET ID_STATO='10',CONTRATTO_NUM='" & TXTCONTRATTO.Text & "',CONTRATTO_DATA='" & par.AggiustaData(TXTDATA.Text) & "',CONTRATTO_DATA_DEC='" & par.AggiustaData(TXTDATADEC.Text) & "' WHERE ID=" & lIdDomanda
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                            & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,24,'" & lblPG.Text & "','" & LblDataPG.Text & "',10," & lblTipoPratica.Text & ",10)"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_cambi (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                            & "','F60','','I')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_ASSEGNATE SET GENERATO_CONTRATTO=1 WHERE ID_DOMANDA=" & lIdDomanda & " AND N_OFFERTA=" & lIdOfferta & " AND PROVENIENZA='G'"
                        par.cmd.ExecuteNonQuery()

                        ImgSalva.Enabled = False

                        par.myTrans.Commit()
                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                        Session.Item("LAVORAZIONE") = "0"
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Contratto inserito!');document.location.href='pagina_home.aspx'" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                        End If
                    End If

                Catch ex As Exception
                    Label15.Text = "ERRORE! I dati non sono stati salvati!</br>" & UCase(ex.Message)
                    Label15.Visible = True
                    par.myTrans.Rollback()
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                End Try

            Else
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Inserire numero contratto');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        End If
    End Sub

    Protected Sub ImgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEsci.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                par.OracleConn.Close()
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.myTrans.Rollback()

                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Catch EX As Exception
            Session.Item("LAVORAZIONE") = "0"
            Page.Dispose()
            Response.Write("<script>document.location.href=""../ErrorPage.aspx""</script>")
        Finally

        End Try
    End Sub
End Class
