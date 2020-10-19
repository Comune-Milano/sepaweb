
Partial Class ASS_ListaAlloggi
    Inherits PageSetIdMode
    Dim par As New CM.Global()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            lIdDomanda = Request.QueryString("ID")

            Visualizza()

        End If
    End Sub

    Public Property sZona() As String
        Get
            If Not (ViewState("par_sZona") Is Nothing) Then
                Return CStr(ViewState("par_sZona"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZona") = value
        End Set

    End Property

    Private Function Visualizza()

        Dim M As Boolean
        Dim sStringaSQL2 As String

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)



            Dim EQ As String = "0"
            Dim POR As String = "0"
            Dim S As String = ""
            Dim B As Boolean = False

            If Session.Item("ABB_392") = "1" Then
                EQ = "1"
                S = " EQCANONE='1' "
                B = True
            End If
            If Session.Item("ABB_431") = "1" Then
                If B = True Then S = S & " OR "
                POR = "1"
                S = " FL_POR='1' "
            End If

            If Session.Item("ABB_ERP") = "1" Then
                If B = True Then S = S & " OR "
                POR = "1"
                S = " FL_POR='0' OR EQCANONE='0' "
            End If

            If S <> "" Then S = "(" & S & ") AND "


            M = False
            Dim TIPOERP As String = "0"

            If lIdDomanda < 500000 Then

                par.cmd.CommandText = "SELECT TIPO_ALLOGGIO FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    TIPOERP = par.IfNull(myReader2("TIPO_ALLOGGIO"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & lIdDomanda
            Else
                par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_CAMBI WHERE ID_DOMANDA=" & lIdDomanda
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
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

                If par.IfNull(myReader1("PREF_PIANO"), " ") <> " " Then
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

                If par.IfNull(myReader1("PREF_P_RIALZATO"), "0") = "1" Then
                    If M = True Then
                        sZona = sZona & " AND ALLOGGI.PIANO='PR'"
                    Else
                        sZona = " ALLOGGI.PIANO='PR'"
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

                If TIPOERP = "0" Then
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.FL_MOD='0') "
                    Else
                        sZona = " (ALLOGGI.FL_MOD='0') "
                        M = True
                    End If
                Else
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.FL_MOD='1') "
                    Else
                        sZona = " (ALLOGGI.FL_MOD='1') "
                        M = True
                    End If

                End If

            End If
            myReader1.Close()

            If sZona <> "" Then sZona = " and " & sZona

            If sZona <> "" Then
                par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                           & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"", " _
                           & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                           & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE " & S & " TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
                           & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                           & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 " _
                           & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) " & sZona & " ORDER BY ZONA ASC"

                myReader1 = par.cmd.ExecuteReader()
                '<p><b>Lista Alloggi disponibili (compatibili con preferenze utente)</b></p>
                Response.Write("DISPONIBILITA' ALLOGGI IN DATA " & Format(Now, "dd/MM/yyyy"))
                sStringaSQL2 = ""
                While myReader1.Read()
                    sStringaSQL2 = sStringaSQL2 & vbCrLf _
                                & "<tr>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("COD_ALLOGGIO") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("TIPO_VIA") & " " & myReader1("INDIRIZZO") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("ZONA") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("PIANO") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("ELEVATORE") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("NUM_LOCALI") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("SUP") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("DATA_DISPONIBILITA1") & "</font></td>" & vbCrLf _
                                & "<td width='20%'><font face='Arial' size='2'>" & myReader1("PROPRIETA1") & "</font></td>" & vbCrLf _
                                & "</tr>"
                End While
                myReader1.Close()

                If sStringaSQL2 <> "" Then
                    sStringaSQL2 = "<p><b>Lista Alloggi disponibili (compatibili con preferenze utente)</b></p>" & vbCrLf _
                                 & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                                 & "<tr>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>CODICE</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>INDIRIZZO</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>ZONA</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>PIANO</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>ASC.</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>N.VANI</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>SUP.</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>DATA DISP.</font></td>" & vbCrLf _
                                 & "<td width='20%'><font face='Arial' size='2'>PROPRIETA'</font></td>" & vbCrLf _
                                 & "</tr>" & vbCrLf & sStringaSQL2 & "</table>"
                End If
                Response.Write(sStringaSQL2)


            End If


            par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE " & S & " ALLOGGI.FL_MOD='" & TIPOERP & "' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ZONA ASC"
            myReader1 = par.cmd.ExecuteReader()

            sStringaSQL2 = ""
            While myReader1.Read()
                sStringaSQL2 = sStringaSQL2 & vbCrLf _
                            & "<tr>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("COD_ALLOGGIO") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("TIPO_VIA") & " " & myReader1("INDIRIZZO") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("ZONA") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("PIANO") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("ELEVATORE") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("NUM_LOCALI") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("SUP") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("DATA_DISPONIBILITA1") & "</font></td>" & vbCrLf _
                            & "<td width='20%'><font face='Arial' size='2'>" & myReader1("PROPRIETA1") & "</font></td>" & vbCrLf _
                            & "</tr>"
            End While
            myReader1.Close()

            If sStringaSQL2 <> "" Then
                sStringaSQL2 = "<p><b>Lista Alloggi disponibili</b></p>" & vbCrLf _
                             & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                             & "<tr>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>CODICE</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>INDIRIZZO</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>ZONA</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>PIANO</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>ASC.</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>N.VANI</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>SUP.</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>DATA DISP.</font></td>" & vbCrLf _
                             & "<td width='20%'><font face='Arial' size='2'>PROPRIETA'</font></td>" & vbCrLf _
                             & "</tr>" & vbCrLf & sStringaSQL2 & "</table>"
            End If


            Response.Write(sStringaSQL2)

            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            Response.Write(EX1.ToString)
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write("ERRORE DURANTE LA FASE DI LETTURA!")
        End Try
    End Function

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


End Class
