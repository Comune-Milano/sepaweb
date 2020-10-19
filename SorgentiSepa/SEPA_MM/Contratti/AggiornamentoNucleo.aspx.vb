
Partial Class Contratti_AggiornamentoNucleo
    Inherits PageSetIdMode
    Dim par As New CM.Global()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM UTENZA_BANDI ORDER BY ID DESC", cmbBando, "ID", "DESCRIZIONE", True)
        End If

        
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If conferma.Value = 1 Then
            AggiornaNucleo(cmbBando.SelectedValue)
        End If
    End Sub

    Private Sub AggiornaNucleo(ByVal IdBando As Long)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim numCompAU As Integer = 0
            Dim numCompContr As Integer = 0
            Dim id_dich As Long = 0
            Dim idContratto As Long = 0

            IdBando = cmbBando.SelectedValue

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE ID=" & IdBando
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                inizioValidita = par.IfNull(lettore("INIZIO_CANONE"), "")
            End If
            lettore.Close()

            Dim Elenco As String = ""
            For Each Items As ListItem In chkContratti.Items
                If Items.Selected = True Then
                    Elenco = Elenco & Items.Value & ","
                End If
            Next
            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
            End If


            par.cmd.CommandText = "SELECT ID_CONTRATTO,NUM_COMP,ID_DICHIARAZIONE FROM SISCOM_MI.CANONI_EC WHERE ID_DICHIARAZIONE IN " & Elenco & ""
            'par.cmd.CommandText = "SELECT ID_CONTRATTO,NUM_COMP,ID_DICHIARAZIONE FROM CANONI_EC WHERE ID_DICHIARAZIONE=25"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    idContratto = par.IfNull(row.Item("ID_CONTRATTO"), 0)
                    numCompAU = par.IfNull(row.Item("NUM_COMP"), 0)
                    id_dich = par.IfNull(row.Item("ID_DICHIARAZIONE"), 0)
                    'par.cmd.CommandText = "SELECT COUNT(ID_ANAGRAFICA) AS N_COMP_CONTR FROM SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & row.Item("ID_CONTRATTO") & " AND NVL(data_fine,'29991231') >= '" & Format(Now(), "yyyyMMdd") & "'"
                    'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReader.Read Then
                    '    numCompContr = par.IfNull(myReader("N_COMP_CONTR"), 0)
                    'End If
                    'myReader.Close()

                    'If numCompAU > numCompContr Then
                    AggiungiComponente(id_dich, idContratto)
                    'Else
                    'EliminaComponente(id_dich, idContratto)
                    'End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F222','')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET NUCLEO_AGGIORNATO = 1 WHERE ID=" & id_dich
                    par.cmd.ExecuteNonQuery()
                Next

                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione effettuata con successo!')</script>")
                cmbBando.SelectedValue = -1
                lblTitolo.Visible = False
                chkSeleziona.Visible = False
                chkContratti.Visible = False
            Else
                Response.Write("<script>alert('Aggiornamento non riuscito!')</script>")
            End If

            

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property inizioValidita() As String
        Get
            If Not (ViewState("par_inizioValidita") Is Nothing) Then
                Return CStr(ViewState("par_inizioValidita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_inizioValidita") = value
        End Set

    End Property

    Private Sub ConfrontaComponente(ByVal r As Data.DataRow, ByVal idContratto As Long, ByVal idAnagrafico As Long)

        Dim cittadinanza As String = ""
        Dim residenza As String = ""
        Dim comuresid As String = ""
        Dim provresid As String = ""
        Dim indirizzresid As String = ""
        Dim idrecapito As String = ""
        Dim idindrecapito As String = ""
        Dim codComuRecap As String = ""

        Try
            '*************** campo CITTADINANZA **************
            par.cmd.CommandText = "select * from comuni_nazioni where cod='" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore("cod"), "").ToString.Contains("Z") Then
                    cittadinanza = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
                Else
                    cittadinanza = "ITALIA"
                End If
            End If
            lettore.Close()
            '*************** fine CITTADINANZA **********


            '*************** campo RESIDENZA **************
            par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(r.Item("id_luogo_res_dnte"), "")
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                comuresid = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
                provresid = par.IfNull(lettore("sigla"), "")
                residenza = comuresid & " (" & provresid & ") CAP " & par.IfNull(r.Item("cap_res_dnte"), "") & " "
            End If
            lettore.Close()
            par.cmd.CommandText = "select * from t_tipo_indirizzo where cod='" & par.IfNull(r.Item("id_tipo_ind_res_dnte"), "") & "'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                indirizzresid = par.PulisciStrSql(par.IfNull(lettore("descrizione"), "")) & " " & par.PulisciStrSql(par.IfNull(r.Item("ind_res_dnte"), ""))
                residenza &= indirizzresid & ", " & par.IfNull(r.Item("civico_res_dnte"), "")
            End If
            lettore.Close()
            '*************** fine RESIDENZA **************


            '********* campo ID_INDIRIZZO_RECAPITO **********
            par.cmd.CommandText = "select SISCOM_MI.SEQ_INDIRIZZI.nextval from dual"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idindrecapito = lettore(0)
            End If
            lettore.Close()

            par.cmd.CommandText = "select * from SISCOM_MI.rapporti_utenza where id=" & idContratto
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                par.cmd.CommandText = "select cod from comuni_nazioni where nome = '" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "'"
                Dim lettoreCod As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreCod.Read Then
                    codComuRecap = par.IfNull(lettoreCod("cod"), "")
                End If
                lettoreCod.Close()

                par.cmd.CommandText = "insert into SISCOM_MI.indirizzi_anagrafica (id,descrizione,civico,cap,localita," _
                    & "cod_comune) values (" & idindrecapito & ",'" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                    & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "','" & par.IfNull(lettore("civico_cor"), "") & "','" & par.IfNull(lettore("cap_cor"), "") & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "','" & codComuRecap & "') "
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select id from SISCOM_MI.indirizzi_anagrafica where descrizione='" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                    & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "' and civico ='" & par.IfNull(lettore("civico_cor"), "") & "'"
                Dim lettoreInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreInd.Read Then
                    idrecapito = par.IfNull(lettoreInd("id"), " ")
                End If
                lettoreInd.Close()
            End If
            lettore.Close()
            '************** fine ID_INDIRIZZO_RECAPITO *************



            '************* AGGIORNAMENTO IN ANAGRAFICA **************

            par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA SET " _
                & "COGNOME= '" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("COGNOME"), "")))) & "'," _
                & "NOME= '" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("NOME"), "")))) & "'," _
                & "COD_FISCALE= '" & par.PulisciStrSql(par.IfNull(r.Item("cod_fiscale"), "")) & "'," _
                & "CITTADINANZA= '" & cittadinanza & "'," _
                & "RESIDENZA= '" & residenza & "'," _
                & "DATA_NASCITA= '" & par.IfNull(r.Item("DATA_NASCITA"), "") & "'," _
                & "COD_COMUNE_NASCITA= '" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'," _
                & "SESSO= '" & par.PulisciStrSql(par.IfNull(r.Item("sesso"), "")) & "'," _
                & "ID_INDIRIZZO_RECAPITO = '" & idrecapito & "'," _
                & "COMUNE_RESIDENZA = '" & comuresid & "'," _
                & "PROVINCIA_RESIDENZA= '" & provresid & "'," _
                & "INDIRIZZO_RESIDENZA= '" & indirizzresid & "'," _
                & "CIVICO_RESIDENZA= '" & par.IfNull(r.Item("civico_res_dnte"), "") & "'," _
                & "CAP_RESIDENZA= '" & par.IfNull(r.Item("cap_res_dnte"), "") & "'," _
                & "TIPO_R=0 " _
                & "WHERE ID= " & idAnagrafico
            par.cmd.ExecuteNonQuery()
            '************* fine INSERIMENTO IN ANAGRAFICA **************

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EliminaComponente(ByVal idDichiarazione As Long, ByVal idContratto As Long)
        Try
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND ID_CONTRATTO=" & idContratto
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE COD_FISCALE='" & par.IfNull(row.Item("COD_FISCALE"), "") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read = False Then
                        If par.IfNull(row.Item("COD_TIPOLOGIA_OCCUPANTE"), "") <> "INTE" Then
                            par.cmd.CommandText = "UPDATE siscom_mi.SOGGETTI_CONTRATTUALI SET DATA_FINE = '" & inizioValidita & "' WHERE ID_ANAGRAFICA = " & row.Item("ID_ANAGRAFICA") & " AND ID_CONTRATTO = " & idContratto
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                    myReader.Close()
                Next
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub AggiungiComponente(ByVal idDichiarazione As Long, ByVal idContratto As Long)
        Try
            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID,id_dichiarazione,cod_fiscale,cognome,nome,sesso,data_nascita,perc_inval,indennita_acc,grado_parentela," _
                & "UTENZA_DICHIARAZIONI.id_luogo_res_dnte,UTENZA_DICHIARAZIONI.id_tipo_ind_res_dnte,UTENZA_DICHIARAZIONI.ind_res_dnte,UTENZA_DICHIARAZIONI.civico_res_dnte,UTENZA_DICHIARAZIONI.cap_res_dnte " _
                & "FROM UTENZA_COMP_NUCLEO,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.id_dichiarazione " _
                & "AND id_dichiarazione = " & idDichiarazione
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows

                    par.cmd.CommandText = "select id from siscom_mi.anagrafica where cod_fiscale = '" & par.IfNull(row.Item("COD_FISCALE"), "") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim idAnagrafico As Long = 0
                    Dim codParentela As String = ""
                    If myReader.Read Then
                        idAnagrafico = par.IfNull(myReader("ID"), 0)
                    End If
                    myReader.Close()

                    If idAnagrafico = 0 Then
                        idAnagrafico = InserInAnagrafica(row, idContratto)
                    End If

                    '****** GRADO PARENTELA ******
                    par.cmd.CommandText = "select * from t_tipo_parentela where cod=" & par.IfNull(row.Item("grado_parentela"), "")
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        codParentela = par.IfNull(myReader("cod_siscom_mi"), "")
                    End If
                    myReader.Close()
                    '****** fine GRADO PARENTELA ******

                    par.cmd.CommandText = "select * from SISCOM_MI.soggetti_contrattuali where id_contratto=" & idContratto & " AND ID_ANAGRAFICA=" & idAnagrafico
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read = False Then
                        par.cmd.CommandText = "insert into SISCOM_MI.soggetti_contrattuali " _
                        & "(id_anagrafica,id_contratto,cod_tipologia_parentela,cod_tipologia_occupante,cod_tipologia_titolo,data_inizio,data_fine) values" _
                        & "(" & idAnagrafico & "," & idContratto & ",'" & codParentela & "','ALTR','LEGIT','" & inizioValidita & "','29991231')"
                        par.cmd.ExecuteNonQuery()
                    Else
                        ConfrontaComponente(row, idContratto, idAnagrafico)
                    End If

                Next
            End If

            EliminaComponente(idDichiarazione, idContratto)

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function InserInAnagrafica(ByVal r As Data.DataRow, ByVal idContratto As Long)
        Dim IdAna As String = ""
        Dim cittadinanza As String = ""
        Dim residenza As String = ""
        Dim comuresid As String = ""
        Dim provresid As String = ""
        Dim indirizzresid As String = ""
        Dim idrecapito As String = ""
        Dim idindrecapito As String = ""
        Dim codComuRecap As String = ""

        Try
            '*************** campo CITTADINANZA **************
            par.cmd.CommandText = "select * from comuni_nazioni where cod='" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore("cod"), "").ToString.Contains("Z") Then
                    cittadinanza = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
                Else
                    cittadinanza = "ITALIA"
                End If
            End If
            lettore.Close()
            '*************** fine CITTADINANZA **********


            '*************** campo RESIDENZA **************
            par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(r.Item("id_luogo_res_dnte"), "")
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                comuresid = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
                provresid = par.IfNull(lettore("sigla"), "")
                residenza = comuresid & " (" & provresid & ") CAP " & par.IfNull(r.Item("cap_res_dnte"), "") & " "
            End If
            lettore.Close()
            par.cmd.CommandText = "select * from SISCOM_MI.t_tipo_indirizzo where cod='" & par.IfNull(r.Item("id_tipo_ind_res_dnte"), "") & "'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                indirizzresid = par.PulisciStrSql(par.IfNull(lettore("descrizione"), "")) & " " & par.PulisciStrSql(par.IfNull(r.Item("ind_res_dnte"), ""))
                residenza &= indirizzresid & ", " & par.IfNull(r.Item("civico_res_dnte"), "")
            End If
            lettore.Close()
            '*************** fine RESIDENZA **************


            '********* campo ID_INDIRIZZO_RECAPITO **********
            par.cmd.CommandText = "select SISCOM_MI.SEQ_INDIRIZZI.nextval from dual"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idindrecapito = lettore(0)
            End If
            lettore.Close()

            par.cmd.CommandText = "select * from SISCOM_MI.rapporti_utenza where id=" & idContratto
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                par.cmd.CommandText = "select cod from comuni_nazioni where nome = '" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "'"
                Dim lettoreCod As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreCod.Read Then
                    codComuRecap = par.IfNull(lettoreCod("cod"), "")
                End If
                lettoreCod.Close()

                par.cmd.CommandText = "insert into SISCOM_MI.indirizzi_anagrafica (id,descrizione,civico,cap,localita," _
                    & "cod_comune) values (" & idindrecapito & ",'" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                    & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "','" & par.IfNull(lettore("civico_cor"), "") & "','" & par.IfNull(lettore("cap_cor"), "") & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "','" & codComuRecap & "') "
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select id from SISCOM_MI.indirizzi_anagrafica where descrizione='" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                    & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "' and civico ='" & par.IfNull(lettore("civico_cor"), "") & "'"
                Dim lettoreInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreInd.Read Then
                    idrecapito = par.IfNull(lettoreInd("id"), " ")
                End If
                lettoreInd.Close()
            End If
            lettore.Close()
            '************** fine ID_INDIRIZZO_RECAPITO *************



            '************* INSERIMENTO IN ANAGRAFICA **************
            par.cmd.CommandText = "select SISCOM_MI.SEQ_ANAGRAFICA.nextval from dual"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                IdAna = lettore(0)
            End If
            lettore.Close()
            par.cmd.CommandText = "insert into SISCOM_MI.anagrafica (id,cognome,nome,data_nascita,cod_fiscale,sesso,cod_comune_nascita,cittadinanza,residenza,id_indirizzo_recapito," _
                                & "comune_residenza,provincia_residenza,indirizzo_residenza,civico_residenza,cap_residenza,tipo_r) values " _
                                & "(" & IdAna & ",'" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("COGNOME"), "")))) & "', " _
                                & "'" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("NOME"), "")))) & "', " _
                                & "'" & par.IfNull(r.Item("DATA_NASCITA"), "") & "', " _
                                & "'" & par.PulisciStrSql(par.IfNull(r.Item("cod_fiscale"), "")) & "', " _
                                & "'" & par.PulisciStrSql(par.IfNull(r.Item("sesso"), "")) & "','" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'," _
                                & "'" & cittadinanza & "','" & residenza & "','" & idrecapito & "','" & comuresid & "','" & provresid & "','" & indirizzresid & "'," _
                                & "'" & par.IfNull(r.Item("civico_res_dnte"), "") & "','" & par.IfNull(r.Item("cap_res_dnte"), "") & "',0)"
            par.cmd.ExecuteNonQuery()
            '************* fine INSERIMENTO IN ANAGRAFICA **************

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return IdAna

    End Function

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub chkSeleziona_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkSeleziona.CheckedChanged
        If chkSeleziona.Checked = True Then
            For Each Items As ListItem In chkContratti.Items
                If Items.Enabled = True Then
                    Items.Selected = True
                End If
            Next
        Else
            For Each Items As ListItem In chkContratti.Items
                If Items.Enabled = True Then
                    Items.Selected = False
                End If
            Next
        End If
    End Sub

    Protected Sub cmbBando_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBando.SelectedIndexChanged
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        chkSeleziona.Visible = True
        lblTitolo.Visible = True
        'par.cmd.CommandText = "SELECT id as IDDICH, RAPPORTO AS DESCRIZIONE FROM UTENZA_DICHIARAZIONI WHERE ID=530"
        par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO ||' - '|| ANAGRAFICA.COGNOME ||' '|| ANAGRAFICA.NOME AS DESCRIZIONE,CANONI_EC.ID_DICHIARAZIONE AS IDDICH FROM SISCOM_MI.CANONI_EC,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_TIPOLOGIA_OCCUPANTE='INTE' AND CANONI_EC.ID_CONTRATTO=RAPPORTI_UTENZA.ID and CANONI_EC.id_dichiarazione in (SELECT ID FROM UTENZA_DICHIARAZIONI WHERE ID_BANDO = " & cmbBando.SelectedValue & " AND NUCLEO_AGGIORNATO = 0) and data_Calcolo in (select max(data_calcolo) from siscom_mi.canoni_ec cec where cec.id_contratto=CANONI_EC.id_contratto and cec.id_dichiarazione=canoni_ec.id_dichiarazione) ORDER BY ANAGRAFICA.COGNOME ASC"
        Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dataTable As New Data.DataTable
        dataAdapter.Fill(dataTable)
        If dataTable.Rows.Count > 0 Then
            chkSeleziona.Visible = True
            lblTitolo.Visible = True
            chkContratti.Visible = True
            chkContratti.Items.Clear()
            chkContratti.DataSource = dataTable
            chkContratti.DataTextField = "DESCRIZIONE"
            chkContratti.DataValueField = "IDDICH"
            chkContratti.DataBind()
            lblTitolo.Text = "Rapporti Utenza"
        Else
            chkContratti.Items.Clear()
            chkContratti.Visible = False
            chkSeleziona.Visible = False
            lblTitolo.Text = "Nessun Rapporto Utenza"
        End If

        For Each Items As ListItem In chkContratti.Items
            ControllaCodFiscali(Items.Value)
            'Items.Selected = True
        Next

        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Private Sub ControllaCodFiscali(ByVal idDichiaraz As Long)
        Try
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & idDichiaraz
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    If par.ControllaCF(par.IfNull(row.Item("COD_FISCALE"), "")) = False Then
                        chkContratti.Items.FindByValue(idDichiaraz).Enabled = False
                        chkContratti.Items.FindByValue(idDichiaraz).Text &= " - Errore nei CF dei componenti!!"
                    End If
                Next
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
