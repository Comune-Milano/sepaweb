
Partial Class ANAUT_NotificaISE
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim Ausi As String = "0"
    Dim indicecontratto As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            idd.Value = Request.QueryString("ID")
            idc.Value = Request.QueryString("COD")

            If idc.Value <> "" And Len(idc.Value) = 19 Then
                lblCodContratto.Text = idc.Value

                Try
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader

                    par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & idc.Value & "'"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        indicecontratto = par.IfNull(myReader("id"), "-1")
                    End If
            myReader.Close()

            par.cmd.CommandText = "select fl_ausi from utenza_dichiarazioni where id=" & idd.Value
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If myReader("fl_ausi") = "1" Then
                    Ausi = "1"
                End If
            End If
            myReader.Close()

            'par.cmd.CommandText = "select siscom_mi.Getintestatari(id),tipo_cor,presso_cor,via_cor,civico_cor,cap_cor,luogo_cor,sigla_cor from siscom_mi.rapporti_utenza where cod_contratto='" & idc.Value & "'"
            par.cmd.CommandText = "select ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,tipo_cor,presso_cor,via_cor,civico_cor,cap_cor,luogo_cor,sigla_cor from siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.ANAGRAFICA,siscom_mi.rapporti_utenza where SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND cod_contratto='" & idc.Value & "'"

            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblIntestatario.Text = par.IfNull(myReader(0), "") & " " & par.IfNull(myReader(1), "")
                a.Value = par.Cripta(lblIntestatario.Text)

                lblIndirizzoPostale.Text = par.IfNull(myReader("presso_cor"), "")
                b.Value = par.Cripta(lblIndirizzoPostale.Text)

                lblIndirizzoPostale0.Text = Mid(par.IfNull(myReader("tipo_cor"), "") & " " & par.IfNull(myReader("via_cor"), "") & " " & par.IfNull(myReader("civico_cor"), ""), 1, 30)
                c.Value = par.Cripta(lblIndirizzoPostale0.Text)

                lblIndirizzoPostale1.Text = par.IfNull(myReader("cap_cor"), "") & " " & par.IfNull(myReader("luogo_cor"), "") & " " & par.IfNull(myReader("sigla_cor"), "")
                d.Value = par.Cripta(lblIndirizzoPostale1.Text)

                lblIndirizzoPostale2.Text = ""
                p.Value = ""
                Dim FF As String = ""

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select scale_edifici.descrizione as SCA,unita_immobiliari.*,TIPO_LIVELLO_PIANO.LIVELLO from siscom_mi.UNITA_CONTRATTUALE,siscom_mi.scale_edifici,siscom_mi.unita_immobiliari,SISCOM_MI.TIPO_LIVELLO_PIANO where UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & indicecontratto & " AND unita_immobiliari.id_scala=scale_edifici.id (+) and TIPO_LIVELLO_PIANO.COD=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO "
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    If par.IfNull(myReader1("INTERNO"), "") <> "" Then
                        FF = FF & "INTERNO " & par.IfNull(myReader1("INTERNO"), "") & " "
                    End If
                    If par.IfNull(myReader1("SCA"), "") <> "" Then
                        FF = FF & "SCALA " & par.IfNull(myReader1("SCA"), "") & " "
                    End If
                    If par.IfNull(myReader1("LIVELLO"), -100) <> -100 Then
                        If myReader1("LIVELLO") = CInt(myReader1("LIVELLO")) Then
                            FF = FF & "PIANO " & Replace(myReader1("LIVELLO"), "0", "T") & " "
                        Else
                            FF = FF & "PIANO " & myReader1("LIVELLO") - 0.5 & " "
                        End If
                    End If

                    lblIndirizzoPostale2.Text = Mid(FF, 1, 30)
                    p.Value = par.Cripta(FF)
                End If
                myReader1.Close()



            End If
            myReader.Close()



            'par.cmd.CommandText = "select indirizzi.*,comuni_nazioni.nome,comuni_nazioni.sigla from comuni_nazioni,siscom_mi.indirizzi where comuni_nazioni.cod=indirizzi.cod_comune and indirizzi.id in (select id_indirizzo from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & Mid(idc.Value, 1, 17) & "')"
            par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & indicecontratto
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblIndirizzo.Text = par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("civico"), "")
                xx.Value = par.Cripta(lblIndirizzo.Text)

                lblLocalita.Text = par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("nome"), "") & " " & par.IfNull(myReader("sigla"), "")
                f.Value = par.Cripta(lblLocalita.Text)
            End If
            myReader.Close()

            If Ausi = "0" Then
                'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & Mid(idc.Value, 1, 17) & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
                par.cmd.CommandText = "SELECT TAB_FILIALI.*,INDIRIZZI.descrizione AS descr,INDIRIZZI.civico,INDIRIZZI.cap,INDIRIZZI.localita FROM siscom_mi.INDIRIZZI,siscom_mi.TAB_FILIALI,siscom_mi.COMPLESSI_IMMOBILIARI,siscom_mi.EDIFICI,siscom_mi.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE INDIRIZZI.ID=TAB_FILIALI.id_indirizzo AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & indicecontratto & " AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.id_filiale"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblFiliale.Text = par.IfNull(myReader("nome"), "")
                    g.Value = par.Cripta(lblFiliale.Text)

                    lblEstremi.Text = par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), "")
                    h.Value = par.Cripta(lblEstremi.Text)

                    lblEstremi0.Text = par.IfNull(myReader("cap"), "")
                    i.Value = par.Cripta(lblEstremi0.Text)

                    lblEstremi1.Text = par.IfNull(myReader("localita"), "")
                    l.Value = par.Cripta(lblEstremi1.Text)

                    lblEstremi2.Text = "Tel:" & par.IfNull(myReader("n_telefono"), "") & " - Fax:" & par.IfNull(myReader("n_fax"), "")
                    m.Value = par.Cripta(lblEstremi2.Text)

                    lblEstremi3.Text = "Ref.Amm.:" & Session.Item("NOME_OPERATORE") 'par.IfNull(myReader("ref_amministrativo"), "")
                    n.Value = par.Cripta(Session.Item("NOME_OPERATORE"))

                    Dim cds As String = "GL0000"
                    Dim Responsabile As String = par.IfNull(myReader("responsabile"), "")



                    lblEstremi5.Text = "Resp.:" & Responsabile
                    q.Value = par.Cripta(Responsabile)

                    lblEstremi4.Text = "Acronimo:" & "GL0000" & "/" & par.IfNull(myReader("ACRONIMO"), "")
                    o.Value = par.Cripta("GL0000" & "/" & par.IfNull(myReader("ACRONIMO"), ""))
                End If
                myReader.Close()

            Else
                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali where indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id= 58"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblFiliale.Text = par.IfNull(myReader("nome"), "")
                    g.Value = par.Cripta(lblFiliale.Text)

                    lblEstremi.Text = par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), "")
                    h.Value = par.Cripta(lblEstremi.Text)

                    lblEstremi0.Text = par.IfNull(myReader("cap"), "")
                    i.Value = par.Cripta(lblEstremi0.Text)

                    lblEstremi1.Text = par.IfNull(myReader("localita"), "")
                    l.Value = par.Cripta(lblEstremi1.Text)

                    lblEstremi2.Text = "Tel:" & par.IfNull(myReader("n_telefono"), "") & " - Fax:" & par.IfNull(myReader("n_fax"), "")
                    m.Value = par.Cripta(lblEstremi2.Text)

                    lblEstremi3.Text = "Ref.Amm.:" & Session.Item("NOME_OPERATORE") 'par.IfNull(myReader("ref_amministrativo"), "")
                    n.Value = par.Cripta(Session.Item("NOME_OPERATORE"))

                    Dim cds As String = "GL0000"
                    Dim Responsabile As String = par.IfNull(myReader("RESPONSABILE"), "")

                    lblEstremi5.Text = "Resp.:" & Responsabile
                    q.Value = par.Cripta(Responsabile)

                    lblEstremi4.Text = "Acronimo:" & "GL0000" & "/" & par.IfNull(myReader("ACRONIMO"), "")
                    o.Value = par.Cripta("GL0000" & "/" & par.IfNull(myReader("ACRONIMO"), ""))
                End If
                myReader.Close()
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch ex As Exception
                lblErrore.Visible = True
                lblErrore.Text = ex.Message

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
            Else

                If Request.QueryString("45") = "1" Then


                    Try
                        If par.OracleConn.State = Data.ConnectionState.Closed Then
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                        End If

                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader

                        'par.cmd.CommandText = "select ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,tipo_cor,presso_cor,via_cor,civico_cor,cap_cor,luogo_cor,sigla_cor from siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.ANAGRAFICA,siscom_mi.rapporti_utenza where SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND cod_contratto='" & idc.Value & "'"
                        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.PG,T_TIPO_INDIRIZZO.descrizione,ind_res_dnte,civico_res_dnte,COMUNI_NAZIONI.nome AS comune,COMUNI_NAZIONI.sigla,cap_res_dnte,scala,alloggio,piano,UTENZA_COMP_NUCLEO.nome,UTENZA_COMP_NUCLEO.cognome " _
                                            & "FROM UTENZA_COMP_NUCLEO, COMUNI_NAZIONI, T_TIPO_INDIRIZZO, UTENZA_DICHIARAZIONI " _
                                            & "WHERE UTENZA_COMP_NUCLEO.id_dichiarazione=UTENZA_DICHIARAZIONI.ID AND UTENZA_COMP_NUCLEO.progr=0 AND " _
                                            & "COMUNI_NAZIONI.ID = UTENZA_DICHIARAZIONI.id_luogo_res_dnte And T_TIPO_INDIRIZZO.cod = UTENZA_DICHIARAZIONI.id_tipo_ind_res_dnte " _
                                            & "AND UTENZA_DICHIARAZIONI.ID=" & idc.Value


                        myReader = par.cmd.ExecuteReader()
                        If myReader.Read Then

                            lblCodContratto.Text = par.IfNull(myReader("PG"), "")

                            lblIntestatario.Text = par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")
                            a.Value = par.Cripta(lblIntestatario.Text)

                            lblIndirizzoPostale.Text = par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")
                            b.Value = par.Cripta(lblIndirizzoPostale.Text)

                            lblIndirizzoPostale0.Text = Mid(par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("ind_res_dnte"), "") & " " & par.IfNull(myReader("civico_res_dnte"), ""), 1, 30)
                            c.Value = par.Cripta(lblIndirizzoPostale0.Text)

                            lblIndirizzoPostale1.Text = par.IfNull(myReader("cap_res_dnte"), "") & " " & par.IfNull(myReader("comune"), "") & " " & par.IfNull(myReader("sigla"), "")
                            d.Value = par.Cripta(lblIndirizzoPostale1.Text)

                            lblIndirizzoPostale2.Text = ""
                            p.Value = ""
                            Dim FF As String = ""

                            If par.IfNull(myReader("alloggio"), "") <> "" Then
                                FF = FF & "INTERNO " & par.IfNull(myReader("alloggio"), "") & " "
                            End If
                            If par.IfNull(myReader("SCAla"), "") <> "" Then
                                FF = FF & "SCALA " & par.IfNull(myReader("SCALA"), "") & " "
                            End If
                            If par.IfNull(myReader("piano"), "") <> "" Then
                                FF = FF & "PIANO " & myReader("piano") & " "
                            End If
                            lblIndirizzoPostale2.Text = Mid(FF, 1, 30)
                            p.Value = par.Cripta(FF)

                            lblIndirizzo.Text = Mid(par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("ind_res_dnte"), "") & " " & par.IfNull(myReader("civico_res_dnte"), ""), 1, 30)
                            xx.Value = par.Cripta(lblIndirizzo.Text)

                            lblLocalita.Text = par.IfNull(myReader("cap_res_dnte"), "") & " " & par.IfNull(myReader("comune"), "") & " " & par.IfNull(myReader("sigla"), "")
                            f.Value = par.Cripta(lblLocalita.Text)


                        End If
                        myReader.Close()

                        If Ausi = "0" Then
                            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & Mid(idc.Value, 1, 17) & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
                            par.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr,indirizzi.civico,indirizzi.cap,indirizzi.localita FROM siscom_mi.indirizzi,siscom_mi.tab_filiali WHERE indirizzi.ID=tab_filiali.id_indirizzo AND tab_filiali.ID=" & Session.Item("ID_STRUTTURA")
                            myReader = par.cmd.ExecuteReader()
                            If myReader.Read Then
                                lblFiliale.Text = par.IfNull(myReader("nome"), "")
                                g.Value = par.Cripta(lblFiliale.Text)

                                lblEstremi.Text = par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), "")
                                h.Value = par.Cripta(lblEstremi.Text)

                                lblEstremi0.Text = par.IfNull(myReader("cap"), "")
                                i.Value = par.Cripta(lblEstremi0.Text)

                                lblEstremi1.Text = par.IfNull(myReader("localita"), "")
                                l.Value = par.Cripta(lblEstremi1.Text)

                                lblEstremi2.Text = "Tel:" & par.IfNull(myReader("n_telefono"), "") & " - Fax:" & par.IfNull(myReader("n_fax"), "")
                                m.Value = par.Cripta(lblEstremi2.Text)

                                lblEstremi3.Text = "Ref.Amm.:" & Session.Item("NOME_OPERATORE")
                                n.Value = par.Cripta(Session.Item("NOME_OPERATORE"))

                                Dim cds As String = "GL0000"
                                Dim Responsabile As String = par.IfNull(myReader("responsabile"), "")

                                

                                lblEstremi4.Text = "Resp.:" & Responsabile
                                q.Value = par.Cripta(Responsabile)

                                lblEstremi5.Text = "Acronimo:" & cds & "/" & par.IfNull(myReader("ACRONIMO"), "")
                                o.Value = par.Cripta(cds & "/" & par.IfNull(myReader("ACRONIMO"), ""))
                            End If
                            myReader.Close()

                        Else
                            par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali where indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id= 58"
                            myReader = par.cmd.ExecuteReader()
                            If myReader.Read Then
                                lblFiliale.Text = par.IfNull(myReader("nome"), "")
                                g.Value = par.Cripta(lblFiliale.Text)

                                lblEstremi.Text = par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), "")
                                h.Value = par.Cripta(lblEstremi.Text)

                                lblEstremi0.Text = par.IfNull(myReader("cap"), "")
                                i.Value = par.Cripta(lblEstremi0.Text)

                                lblEstremi1.Text = par.IfNull(myReader("localita"), "")
                                l.Value = par.Cripta(lblEstremi1.Text)

                                lblEstremi2.Text = "Tel:" & par.IfNull(myReader("n_telefono"), "") & " - Fax:" & par.IfNull(myReader("n_fax"), "")
                                m.Value = par.Cripta(lblEstremi2.Text)

                                lblEstremi3.Text = "Ref.Amm.:" & Session.Item("NOME_OPERATORE") 'par.IfNull(myReader("ref_amministrativo"), "")
                                n.Value = par.Cripta(Session.Item("NOME_OPERATORE"))

                                Dim cds As String = "GL0000"
                                Dim Responsabile As String = par.IfNull(myReader("RESPONSABILE"), "")

                                lblEstremi5.Text = "Resp.:" & Responsabile
                                q.Value = par.Cripta(Responsabile)

                                lblEstremi4.Text = "Acronimo:" & "GL0000" & "/" & par.IfNull(myReader("ACRONIMO"), "")
                                o.Value = par.Cripta("GL0000" & "/" & par.IfNull(myReader("ACRONIMO"), ""))
                            End If
                            myReader.Close()

                        End If

                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Catch ex As Exception
                        lblErrore.Visible = True
                        lblErrore.Text = ex.Message

                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End Try
                End If
            End If

            If Ausi = "1" Then
                AU.Value = "1"
            Else
                AU.Value = "0"
            End If

            If lblIntestatario.Text = "" Or lblIndirizzoPostale.Text = "" Or lblIndirizzoPostale0.Text = "" Or lblIndirizzoPostale1.Text = "" Or lblIndirizzo.Text = "" Or lblLocalita.Text = "" Or lblFiliale.Text = "" Or lblEstremi.Text = "" Or lblEstremi0.Text = "" Or lblEstremi1.Text = "" Or lblEstremi2.Text = "" Or lblEstremi3.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Dati mancanti!! Non è possibile procedere"

            End If
        End If
    End Sub


End Class
