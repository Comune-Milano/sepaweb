
Partial Class ASS_AbbinaDomanda
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
            lIdDomanda = Request.QueryString("ID")
            HiddenField1.Value = Request.QueryString("TIPO")

            Select Case Request.QueryString("TIPO")
                Case "1"
                    VisualizzaDomanda()
                Case "2"
                    VisualizzaDomandaCambi()
                Case "3"
                    VisualizzaDomandaEmergenze()
            End Select
            txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

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

    Private Function VisualizzaDomandaEmergenze()
        Dim scriptblock As String
        'Dim M As Boolean

        Try
            imgPreferenze.Visible = False

            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE123", par.OracleConn)


            sTipoAlloggio = "0"

            lblVisEstremi.Attributes.Add("onclick", "javascript:window.open('../EstremiDocumento.aspx?T=3&ID=" & lIdDomanda & "&I=" & par.Cripta(lblNominativo.Text) & "','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")

            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_vsa WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                myReader.Close()

                par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_vsa.DATA_PG,DOMANDE_BANDO_vsa.id_bando,DOMANDE_BANDO_vsa.PG,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME,domande_bando_vsa.FL_ASS_ESTERNA,domande_bando_vsa.id_dichiarazione FROM DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa WHERE DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_vsa.PROGR=DOMANDE_BANDO_vsa.PROGR_COMPONENTE AND DOMANDE_BANDO_vsa.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                        Button1.Enabled = False
                    End If
                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 27)
                    lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")

                    lblIsbarcr.Text = "//" 'par.IfNull(myReader1("isbarc_r"), "0")
                    lblISEE.Text = par.IfNull(myReader1("reddito_isee"), "0")
                    lblDataPg.value = par.IfNull(myReader1("DATA_PG"), "0")
                    lblTipologia.value = par.IfNull(myReader1("id_bando"), "0")



                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE comp_nucleo_vsa.perc_inval>=66 and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                lblInvalidi.Text = "0"
                If myReader1.Read() Then
                    lblInvalidi.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()


                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO_vsa.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                lblAnziani.Text = "0"
                If myReader1.Read() Then
                    lblAnziani.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT PREF_H_MOTORIO FROM domande_preferenze_vsa WHERE ID_domanda=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                LBLmotorio.Visible = False
                If myReader1.Read() Then
                    If myReader1(0) = "0" Then
                        LBLmotorio.Visible = False
                    Else
                        LBLmotorio.Visible = True
                    End If
                End If
                myReader1.Close()

                Dim M As Boolean = False
                par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_vsa WHERE ID_DOMANDA=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    sZona = ""
                    If par.IfNull(myReader1("PREF_ZONA1"), " ") <> " " Then
                        sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA1"), " ") & "'"
                        M = True
                    End If
                    If par.IfNull(myReader1("PREF_ZONA2"), " ") <> " " Then
                        If M = False Then
                            sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA2"), " ") & "'"
                            M = True
                        Else
                            sZona = sZona & " OR ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA2"), " ") & "'"
                            M = True
                        End If
                    End If

                    If par.IfNull(myReader1("PREF_ZONA3"), " ") <> " " Then
                        If M = False Then
                            sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA3"), " ") & "'"
                            M = True
                        Else
                            sZona = sZona & " OR ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA3"), " ") & "'"
                            M = True
                        End If
                    End If

                    If M = True Then sZona = sZona & ") "

                    If par.IfNull(myReader1("PREF_PIANO"), " ") <> " " And par.IfNull(myReader1("PREF_PIANO"), " ") <> "-1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.PIANO='" & par.IfNull(myReader1("PREF_PIANO"), " ") & "'"
                        Else
                            sZona = " ALLOGGI.PIANO='" & par.IfNull(myReader1("PREF_PIANO"), " ") & "'"
                            M = True
                        End If
                    End If


                    If par.IfNull(myReader1("PREF_H_MOTORIO"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.H_MOTORIO='1'"
                        Else
                            sZona = " ALLOGGI.H_MOTORIO='1'"
                            M = True
                        End If
                    End If



                    If par.IfNull(myReader1("PREF_ASCENSORE"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.ASCENSORE='1'"
                        Else
                            sZona = " ALLOGGI.ASCENSORE='1'"
                            M = True
                        End If
                    End If


                    If par.IfNull(myReader1("PREF_BARRIERE"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                        Else
                            sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            M = True
                        End If
                    End If

                End If
                myReader1.Close()
                myReader.Close()






                'imgPreferenze.Attributes.Add("OnClick", "javascript:window.open('Preferenze.aspx?T=2" & "ID=" & lIdDomanda & "&PG=" & lblPG.Text & "','Preferenze','top=0,left=0,width=600,height=400');")

                'BindGrid1()
                'BindGrid()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE123", par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            Else
                myReader.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile abbinare in questo momento!');history.go(-1);" _
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



    Private Function VisualizzaDomandaCambi()
        Dim scriptblock As String
        'Dim M As Boolean

        Try
            imgPreferenze.Visible = False

            sTipoAlloggio = "0"

            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE123", par.OracleConn)

            'lblVisDomanda.Text = "<a href=" & Chr(34) & "javascript:window.open('../CAMBI/domanda.aspx?ID=" & lIdDomanda & "&LE=1&US=1','','top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Vis. Domanda</a>"
            lblVisDomanda.Attributes.Add("onclick", "javascript:window.open('../CAMBI/domanda.aspx?ID=" & lIdDomanda & "&LE=1&US=1','','top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
            lblVisEstremi.Attributes.Add("onclick", "javascript:window.open('../EstremiDocumento.aspx?T=2&ID=" & lIdDomanda & "&I=" & par.Cripta(lblNominativo.Text) & "','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")

            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_CAMBI WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                myReader.Close()
                par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_CAMBI.DATA_PG,DOMANDE_BANDO_CAMBI.id_bando,DOMANDE_BANDO_CAMBI.PG,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME,domande_bando_CAMBI.FL_ASS_ESTERNA,domande_bando_cambi.id_dichiarazione FROM DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI WHERE DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_CAMBI.PROGR=DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE AND DOMANDE_BANDO_CAMBI.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                        Button1.Enabled = False
                    End If
                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 27)
                    lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")

                    lblIsbarcr.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    lblISEE.Text = par.IfNull(myReader1("reddito_isee"), "0")
                    lblDataPg.value = par.IfNull(myReader1("DATA_PG"), "0")
                    lblTipologia.value = par.IfNull(myReader1("id_bando"), "0")

                    lblVisDichiarazione.Attributes.Add("onclick", "javascript:window.open('../CAMBI/max.aspx?ID=" & par.IfNull(myReader1("id_dichiarazione"), "0") & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")

                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_CAMBI.ID) FROM COMP_NUCLEO_CAMBI,dichiarazioni_CAMBI,domande_bando_CAMBI WHERE DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND DOMANDE_BANDO_CAMBI.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_CAMBI.ID) FROM COMP_NUCLEO_CAMBI,dichiarazioni_CAMBI,domande_bando_CAMBI WHERE comp_nucleo_CAMBI.perc_inval>=66 and DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND DOMANDE_BANDO_CAMBI.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                lblInvalidi.Text = "0"
                If myReader1.Read() Then
                    lblInvalidi.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()


                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_CAMBI.ID) FROM COMP_NUCLEO_CAMBI,dichiarazioni_CAMBI,domande_bando_CAMBI WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO_CAMBI.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND DOMANDE_BANDO_CAMBI.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                lblAnziani.Text = "0"
                If myReader1.Read() Then
                    lblAnziani.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT PREF_H_MOTORIO FROM domande_preferenze_CAMBI WHERE ID_domanda=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                LBLmotorio.Visible = False
                If myReader1.Read() Then
                    If myReader1(0) = "0" Then
                        LBLmotorio.Visible = False
                    Else
                        LBLmotorio.Visible = True
                    End If
                End If
                myReader1.Close()

                Dim M As Boolean = False
                par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_CAMBI WHERE ID_DOMANDA=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    sZona = ""
                    If par.IfNull(myReader1("PREF_ZONA1"), " ") <> " " Then
                        sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA1"), " ") & "'"
                        M = True
                    End If
                    If par.IfNull(myReader1("PREF_ZONA2"), " ") <> " " Then
                        If M = False Then
                            sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA2"), " ") & "'"
                            M = True
                        Else
                            sZona = sZona & " OR ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA2"), " ") & "'"
                            M = True
                        End If
                    End If

                    If par.IfNull(myReader1("PREF_ZONA3"), " ") <> " " Then
                        If M = False Then
                            sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA3"), " ") & "'"
                            M = True
                        Else
                            sZona = sZona & " OR ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA3"), " ") & "'"
                            M = True
                        End If
                    End If

                    If M = True Then sZona = sZona & ") "

                    If par.IfNull(myReader1("PREF_PIANO"), " ") <> " " And par.IfNull(myReader1("PREF_PIANO"), " ") <> "-1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.PIANO='" & par.IfNull(myReader1("PREF_PIANO"), " ") & "'"
                        Else
                            sZona = " ALLOGGI.PIANO='" & par.IfNull(myReader1("PREF_PIANO"), " ") & "'"
                            M = True
                        End If
                    End If


                    If par.IfNull(myReader1("PREF_H_MOTORIO"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.H_MOTORIO='1'"
                        Else
                            sZona = " ALLOGGI.H_MOTORIO='1'"
                            M = True
                        End If
                    End If



                    If par.IfNull(myReader1("PREF_ASCENSORE"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.ASCENSORE='1'"
                        Else
                            sZona = " ALLOGGI.ASCENSORE='1'"
                            M = True
                        End If
                    End If


                    If par.IfNull(myReader1("PREF_BARRIERE"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                        Else
                            sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            M = True
                        End If
                    End If


                End If
                myReader1.Close()
                myReader.Close()


                imgPreferenze.Attributes.Add("OnClick", "javascript:window.open('GestionePreferenze.aspx?T=1&PROV=0&ID=" & lIdDomanda & "&PG=" & lblPG.Text & "', 'Preferenze', 'height=620,top=0,left=0,width=800,scrollbars=no');")



                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE123", par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            Else
                myReader.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile abbinare in questo momento!');history.go(-1);" _
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
        Dim M As Boolean

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE123", par.OracleConn)
            lblVisDomanda.Attributes.Add("onclick", "javascript:window.open('../domanda.aspx?ID=" & lIdDomanda & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
            lblVisEstremi.Attributes.Add("onclick", "javascript:window.open('../EstremiDocumento.aspx?T=1&ID=" & lIdDomanda & "&I=" & par.Cripta(lblNominativo.Text) & "','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")

            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                myReader.Close()
                par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO.DATA_PG,DOMANDE_BANDO.id_bando,DOMANDE_BANDO.PG,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,domande_bando.FL_ASS_ESTERNA,domande_bando.id_dichiarazione,domande_bando.tipo_alloggio FROM DOMANDE_BANDO,COMP_NUCLEO WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND DOMANDE_BANDO.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda NON è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                        Button1.Enabled = False
                    End If

                    sTipoAlloggio = par.IfNull(myReader1("tipo_alloggio"), "0")

                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 27)
                    lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")

                    lblIsbarcr.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    lblISEE.Text = par.IfNull(myReader1("reddito_isee"), "0")
                    lblDataPg.value = par.IfNull(myReader1("DATA_PG"), "0")
                    lblTipologia.value = par.IfNull(myReader1("id_bando"), "0")

                    lblVisDichiarazione.Attributes.Add("onclick", "javascript:window.open('../max.aspx?ID=" & par.IfNull(myReader1("id_dichiarazione"), "0") & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")

                End If
                myReader1.Close()

                lblTipoAlloggio.Visible = True
                Label14.Visible = True

                If sTipoAlloggio = "0" Then
                    lblTipoAlloggio.Text = "A"
                Else
                    lblTipoAlloggio.Text = "B"
                End If

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE comp_nucleo.perc_inval>=66 and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                lblInvalidi.Text = "0"
                If myReader1.Read() Then
                    lblInvalidi.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()


                par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                lblAnziani.Text = "0"
                If myReader1.Read() Then
                    lblAnziani.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT PREF_H_MOTORIO FROM domande_preferenze WHERE ID_domanda=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                LBLmotorio.Visible = False
                If myReader1.Read() Then
                    If myReader1(0) = "0" Then
                        LBLmotorio.Visible = False
                    Else
                        LBLmotorio.Visible = True
                    End If
                End If
                myReader1.Close()

                M = False
                par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    sZona = ""
                    If par.IfNull(myReader1("PREF_ZONA1"), " ") <> " " Then
                        sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA1"), " ") & "'"
                        M = True
                    End If
                    If par.IfNull(myReader1("PREF_ZONA2"), " ") <> " " Then
                        If M = False Then
                            sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA2"), " ") & "'"
                            M = True
                        Else
                            sZona = sZona & " OR ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA2"), " ") & "'"
                            M = True
                        End If
                    End If

                    If par.IfNull(myReader1("PREF_ZONA3"), " ") <> " " Then
                        If M = False Then
                            sZona = sZona & "(ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA3"), " ") & "'"
                            M = True
                        Else
                            sZona = sZona & " OR ALLOGGI.ZONA='" & par.IfNull(myReader1("PREF_ZONA3"), " ") & "'"
                            M = True
                        End If
                    End If

                    If M = True Then sZona = sZona & ") "

                    If par.IfNull(myReader1("PREF_PIANO"), " ") <> " " And par.IfNull(myReader1("PREF_PIANO"), " ") <> "-1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.PIANO='" & par.IfNull(myReader1("PREF_PIANO"), " ") & "'"
                        Else
                            sZona = " ALLOGGI.PIANO='" & par.IfNull(myReader1("PREF_PIANO"), " ") & "'"
                            M = True
                        End If
                    End If


                    If par.IfNull(myReader1("PREF_H_MOTORIO"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.H_MOTORIO='1'"
                        Else
                            sZona = " ALLOGGI.H_MOTORIO='1'"
                            M = True
                        End If
                    End If


                    If par.IfNull(myReader1("PREF_ASCENSORE"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND ALLOGGI.ASCENSORE='1'"
                        Else
                            sZona = " ALLOGGI.ASCENSORE='1'"
                            M = True
                        End If
                    End If


                    If par.IfNull(myReader1("PREF_BARRIERE"), "0") = "1" Then
                        If M = True Then
                            sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                        Else
                            sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            M = True
                        End If
                    End If

                    If sTipoAlloggio = "1" Then
                        If M = True Then
                            sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
                        Else
                            sZona = " (ALLOGGI.fl_mod='1') "
                            M = True
                        End If
                    Else
                        If M = True Then
                            sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
                        Else
                            sZona = " (ALLOGGI.fl_mod='0') "
                            M = True
                        End If
                    End If

                End If
                myReader1.Close()
                myReader.Close()


                imgPreferenze.Attributes.Add("OnClick", "javascript:window.open('GestionePreferenze.aspx?T=1&PROV=0&ID=" & lIdDomanda & "&PG=" & lblPG.Text & "', 'Preferenze', 'height=620,top=0,left=0,width=800,scrollbars=no');")
               

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE123", par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            Else
                myReader.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile abbinare in questo momento!');history.go(-1);" _
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




    'Private Sub BindGrid1()
    '    Dim EQ As String = "0"
    '    Dim POR As String = "0"
    '    Dim S As String = ""
    '    Dim B As Boolean = False

    '    If sZONA = "" Then
    '        DataGrid2.Visible = False
    '        Label10.Visible = False
    '        Exit Sub
    '    End If

    '    If sZona <> "" Then
    '        If Mid(sZona, 1, 4) <> " and" Then
    '            sZona = " and " & sZona
    '        End If
    '    End If


    '    sStringaSQL2 = "SELECT (SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV,DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
    '               & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"", " _
    '               & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
    '               & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici WHERE " & S & " ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND  FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
    '               & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
    '               & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
    '               & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) " & sZONA & " ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC"

    '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)

    '    Dim ds As New Data.DataSet()
    '    da.Fill(ds, "ALLOGGI")
    '    DataGrid2.DataSource = ds
    '    DataGrid2.DataBind()

    'End Sub

    'Private Sub BindGrid()

    '    Dim EQ As String = "0"
    '    Dim POR As String = "0"
    '    Dim S As String = ""
    '    Dim B As Boolean = False


    '    sStringaSQL1 = "SELECT (SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV,DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
    '               & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"", " _
    '               & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
    '               & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE " & S & " ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
    '               & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
    '               & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
    '               & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC"


    '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

    '    Dim ds As New Data.DataSet()
    '    da.Fill(ds, "ALLOGGI")
    '    DataGrid1.DataSource = ds
    '    DataGrid1.DataBind()


    'End Sub


    Public Property sTipoAlloggio() As String
        Get
            If Not (ViewState("par_sTipoAlloggio") Is Nothing) Then
                Return CStr(ViewState("par_sTipoAlloggio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sTipoAlloggio") = value
        End Set
    End Property

    Public Property sStringaSQL2() As String
        Get
            If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL2") = value
        End Set
    End Property

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set
    End Property

    'Protected Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
    '    Dim scriptblock As String = ""
    '    scriptblock = "<script language='javascript' type='text/javascript'>" _
    '    & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & e.Item.Cells(1).Text & "','" & e.Item.Cells(1).Text & "','height=580,top=0,left=0,width=780');" _
    '    & "</script>"
    '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
    '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
    '    End If
    '    Session.Add("CHIAMATA", 1)
    'End Sub

    'Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    '    LBLID.value = e.Item.Cells(0).Text
    '    Label9.Text = "Hai selezionato Unità Cod.: " & e.Item.Cells(1).Text
    'End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or _
    'e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '    End If
    'End Sub

    'Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then
    '        DataGrid1.CurrentPageIndex = e.NewPageIndex
    '        BindGrid()
    '    End If
    'End Sub

    'Protected Sub DataGrid2_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.CancelCommand
    '    Dim scriptblock As String = ""
    '    scriptblock = "<script language='javascript' type='text/javascript'>" _
    '    & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & e.Item.Cells(1).Text & "','" & e.Item.Cells(1).Text & "','height=580,top=0,left=0,width=780');" _
    '    & "</script>"
    '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
    '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
    '    End If
    '    Session.Add("CHIAMATA", 1)
    'End Sub

    'Protected Sub DataGrid2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.EditCommand
    '    LBLID.value = e.Item.Cells(0).Text
    '    Label9.Text = "Hai selezionato Unità Cod.: " & e.Item.Cells(1).Text
    'End Sub

    '    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
    '        If e.Item.ItemType = ListItemType.Item Or _
    'e.Item.ItemType = ListItemType.AlternatingItem Then
    '            '---------------------------------------------------         
    '            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '            '---------------------------------------------------         
    '            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
    '            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        End If
    '    End Sub

    'Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then
    '        DataGrid2.CurrentPageIndex = e.NewPageIndex
    '        BindGrid1()
    '    End If
    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If LBLID.value = "-1" Or LBLID.value = "" Or LBLID.value = "Label" Then
            Response.Write("<script>alert('Nessun Alloggio Selezionato!')</script>")
        Else
            If Len(txtData.Text) <> 10 Or IsDate(txtData.Text) = False Or par.AggiustaData(txtData.Text) < Format(Now, "yyyyMMdd") Then
                Response.Write("<script>alert('Data Scadenza non valida!')</script>")
            Else
                Select Case HiddenField1.Value
                    Case "1"
                        Abbina()
                    Case "2"
                        AbbinaCambi()
                    Case "3"
                        AbbinaEmergenze()

                End Select

            End If

        End If
    End Sub


    Private Function AbbinaEmergenze()
        Dim scriptblock As String
        Dim DATASCADENZA As String
        Dim N_ABBINAMENTO As String

        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE123"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE123"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.value & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    par.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & lIdDomanda & ",'" & DATASCADENZA & "','1')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_vsa SET FL_PROPOSTA='1' WHERE ID=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & lIdDomanda & "," & LBLID.Value & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & lIdDomanda & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.value & "," _
                                        & lIdDomanda & ",'')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() = False Then
                        par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & lblPG.Text & "','" & lblDataPg.Value & "',10," & lblTipologia.value & ",10)"
                    par.cmd.ExecuteNonQuery()

                    par.myTrans.Commit()

                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE123", par.myTrans)


                    btnReport.Visible = True
                    Button1.Visible = False
                    imgSeleziona.Visible = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio Abbinato con successo! Ora sarà visualizzato il report di abbinamento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    Session.Add("NOMINATIVO", lblNominativo.Text)

                    btnReport.Attributes.Add("OnClick", "javascript:window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.value & "&DATAS=" & txtData.Text & "','Report');")

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.value & "&DATAS=" & txtData.Text & "','Report');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                    End If


                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If
            End If
            myReader.Close()
            'par.Dispose()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            'par.Dispose()
            'par.myTrans.Rollback()
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                'scriptblock = "<script language='javascript' type='text/javascript'>alert('" & EX1.Message & "');</script>"
                'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                'End If
                Panel1.Visible = True
                Label11.Visible = True
                Label12.Visible = True
                Label11.Text = EX1.Message
            End If

        Catch ex As Exception
            Panel1.Visible = True
            Label11.Visible = True
            Label12.Visible = True
            Label11.Text = ex.Message
            'par.myTrans.Rollback()
            'par.Dispose()
            'Label10.Text = ex.ToString
        End Try

    End Function

    Private Function AbbinaCambi()
        Dim scriptblock As String
        Dim DATASCADENZA As String
        Dim N_ABBINAMENTO As String

        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE123"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE123"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.value & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    par.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & lIdDomanda & ",'" & DATASCADENZA & "','1')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_PROPOSTA='1' WHERE ID=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & lIdDomanda & "," & LBLID.Value & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & lIdDomanda & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.value & "," _
                                        & lIdDomanda & ",'')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() = False Then
                        par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & lblPG.Text & "','" & lblDataPg.Value & "',10," & lblTipologia.value & ",10)"
                    par.cmd.ExecuteNonQuery()

                    par.myTrans.Commit()

                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE123", par.myTrans)


                    btnReport.Visible = True
                    Button1.Visible = False
                    imgSeleziona.Visible = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio Abbinato con successo! Ora sarà visualizzato il report di abbinamento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    Session.Add("NOMINATIVO", lblNominativo.Text)

                    btnReport.Attributes.Add("OnClick", "javascript:window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.value & "&DATAS=" & txtData.Text & "','Report');")

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.value & "&DATAS=" & txtData.Text & "','Report');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                    End If


                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If
            End If
            myReader.Close()
            'par.Dispose()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            'par.Dispose()
            'par.myTrans.Rollback()
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                'scriptblock = "<script language='javascript' type='text/javascript'>alert('" & EX1.Message & "');</script>"
                'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                'End If
                Panel1.Visible = True
                Label11.Visible = True
                Label12.Visible = True
                Label11.Text = EX1.Message
            End If

        Catch ex As Exception
            Panel1.Visible = True
            Label11.Visible = True
            Label12.Visible = True
            Label11.Text = ex.Message
            'par.myTrans.Rollback()
            'par.Dispose()
            'Label10.Text = ex.ToString
        End Try

    End Function

    Private Function Abbina()
        Dim scriptblock As String
        Dim DATASCADENZA As String
        Dim N_ABBINAMENTO As String

        Try

            If IsNothing(CType(HttpContext.Current.Session.Item("CONNESSIONE123"), Oracle.DataAccess.Client.OracleConnection)) = False Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE123"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE123"), Oracle.DataAccess.Client.OracleTransaction)
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT STATO,PRENOTATO FROM ALLOGGI WHERE ID=" & LBLID.Value & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" And myReader("PRENOTATO") = "0" Then

                    DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    par.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & lIdDomanda & ",'" & DATASCADENZA & "','1')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA='1' WHERE ID=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & lIdDomanda & "," & LBLID.Value & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & lIdDomanda & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.Value & "," _
                                        & lIdDomanda & ",'')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() = False Then
                        par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & lblPG.Text & "','" & lblDataPg.Value & "',10," & lblTipologia.Value & ",10)"
                    par.cmd.ExecuteNonQuery()

                    par.myTrans.Commit()
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE123", par.myTrans)


                    btnReport.Visible = True
                    Button1.Visible = False
                    imgSeleziona.Visible = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio Abbinato con successo! Ora sarà visualizzato il report di abbinamento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    Session.Add("NOMINATIVO", lblNominativo.Text)

                    btnReport.Attributes.Add("OnClick", "javascript:window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Value & "&DATAS=" & txtData.Text & "','Report');")

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Value & "&DATAS=" & txtData.Text & "','Report');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                    End If


                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If
            End If
            myReader.Close()
            'par.Dispose()

            par.OracleConn.Close()
            par.OracleConn.Dispose()
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            'par.Dispose()
            'par.myTrans.Rollback()
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                'scriptblock = "<script language='javascript' type='text/javascript'>alert('" & EX1.Message & "');</script>"
                'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                'End If
                Panel1.Visible = True
                Label11.Visible = True
                Label12.Visible = True
                Label11.Text = EX1.Message
            End If

        Catch ex As Exception
            Panel1.Visible = True
            Label11.Visible = True
            Label12.Visible = True
            Label11.Text = ex.Message
            'par.myTrans.Rollback()
            'par.Dispose()
            'Label10.Text = ex.ToString
        End Try

    End Function

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Panel1.Visible = False
    End Sub

    'Protected Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
    '    Dim CANONE As Double = 0
    '    Dim S As String = ""
    '    Dim id_unita As Long = 0
    '    If e.Item.Cells(2).Text = "0" Then
    '        Try
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            par.cmd.CommandText = "SELECT id from siscom_MI.unita_immobiliari where cod_unita_immobiliare='" & e.Item.Cells(1).Text & "'"
    '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader.Read() Then
    '                id_unita = par.IfNull(myReader(0), -1)
    '            End If
    '            myReader.Close()
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Catch ex As Exception
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End Try

    '        Dim VAL_LOCATIVO_UNITA As String = ""
    '        Dim comunicazioni As String = ""

    '        Select Case HiddenField1.Value
    '            Case "1"
    '                S = par.CalcolaCanone27(lIdDomanda, 1, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZona, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
    '            Case "2"
    '                S = par.CalcolaCanone27(lIdDomanda, 2, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZona, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
    '            Case "3"
    '                S = par.CalcolaCanone27(lIdDomanda, 3, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZona, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
    '        End Select
    '        If comunicazioni <> "" Then
    '            Response.Write("<script>alert('" & comunicazioni & "');</script>")
    '        End If



    '        Session.Add("canone", S)
    '        Response.Write("<script>popupWindow=window.open('Canone.aspx','Canone','');popupWindow.focus();</script>")
    '    Else
    '        Response.Write("<script>alert('Non è possibile calcolare il canone su questo alloggio perchè non di proprietà comunale');</script>")
    '    End If

    'End Sub

    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sPIANO() As String
        Get
            If Not (ViewState("par_sPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPIANO") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property


    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property


    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property


    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property


    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property



    Public Property CanoneCorrente() As Double
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDbl(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property


    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property


    'Protected Sub DataGrid2_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.UpdateCommand
    '    Dim CANONE As Double = 0
    '    Dim S As String = ""
    '    Dim id_unita As Long = 0
    '    If e.Item.Cells(2).Text = "0" Then
    '        Try
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            par.cmd.CommandText = "SELECT id from siscom_MI.unita_immobiliari where cod_unita_immobiliare='" & e.Item.Cells(1).Text & "'"
    '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader.Read() Then
    '                id_unita = par.IfNull(myReader(0), -1)
    '            End If
    '            myReader.Close()
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Catch ex As Exception
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End Try

    '        Dim VAL_LOCATIVO_UNITA As String = ""
    '        Dim comunicazioni As String = ""
    '        Select Case HiddenField1.Value
    '            Case "1"
    '                S = par.CalcolaCanone27(lIdDomanda, 1, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
    '            Case "2"
    '                S = par.CalcolaCanone27(lIdDomanda, 2, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
    '            Case "3"
    '                S = par.CalcolaCanone27(lIdDomanda, 3, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
    '        End Select
    '        If comunicazioni <> "" Then
    '            Response.Write("<script>alert('" & comunicazioni & "');</script>")
    '        End If

    '        Session.Add("canone", S)
    '        Response.Write("<script>popupWindow=window.open('Canone.aspx','Canone','');popupWindow.focus();</script>")
    '    Else
    '        Response.Write("<script>alert('Non è possibile calcolare il canone su questo alloggio perchè non di proprietà comunale');</script>")
    '    End If

    'End Sub



    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property


    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property

    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set

    End Property


    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property


    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Or Session.Item("CHIAMATA") = "1" Then
                par.OracleConn.Close()
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE123"), Oracle.DataAccess.Client.OracleConnection)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE123"), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
                If Not IsNothing(par.myTrans.Connection) Then
                    par.myTrans.Rollback()
                End If

                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE123")
                HttpContext.Current.Session.Remove("CONNESSIONE123")
                Session.Item("LAVORAZIONE") = "0"
                Session.Item("CHIAMATA") = "0"
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else

                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Catch EX As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Page.Dispose()
            Response.Write("<script>document.location.href=""../ErrorPage.aspx""</script>")
        Finally

        End Try
    End Sub


    Protected Sub imgSeleziona_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSeleziona.Click
        Dim Indice As String = Session.Item("UNITA")

        If Indice <> "" Then
            LBLID.Value = Indice
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT * from alloggi where id=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lblCodice.Text = par.IfNull(myReader("COD_ALLOGGIO"), "---")
                    lblIndirizzo.Text = par.IfNull(myReader("INDIRIZZO"), "---") & " " & par.IfNull(myReader("num_CIVICO"), "---") & " Interno " & par.IfNull(myReader("NUM_ALLOGGIO"), "---")
                    lblpiano.text = par.IfNull(myReader("PIANO"), "---")
                    lblScala.Text = par.IfNull(myReader("SCALA"), "---")
                    Label9.Text = "Unità cod. " & par.IfNull(myReader("COD_ALLOGGIO"), "---")
                End If
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                Label9.Text = ex.Message
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If

    End Sub
End Class