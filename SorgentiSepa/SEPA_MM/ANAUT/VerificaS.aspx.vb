
Partial Class ANAUT_VerificaS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            BindGrid()
        End If
    End Sub

    Public Property sStringaSql1() As String
        Get
            If Not (ViewState("par_sStringaSql1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSql1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSql1") = value
        End Set

    End Property




    Private Sub BindGrid()

        par.OracleConn.Open()
        par.SettaCommand(par)
        par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            IndiceAU = myReader("ID")
        End If
        myReader.Close()


        sStringaSql1 = "SELECT " _
                            & "'VALIDO' AS STATO," _
                            & "RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA," _
                            & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                            & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA  " _
                            & "FROM siscom_mi.unita_contrattuale, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI " _
                            & " " _
                            & "WHERE RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL AND rapporti_utenza.id not in (select nvl(id_contratto,0) from UTENZA_GRUPPI_DICHIARAZIONI where id_gruppo in (select id from UTENZA_GRUPPI_LAVORO where id_bando_au=" & IndiceAU & "))" _
                            & " and UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND " _
                            & "UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL' " _
                            & "AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='NONE' " _
                            & "AND SUBSTR(COD_CONTRATTO,1,2)<>'41' AND SUBSTR(COD_CONTRATTO,1,2)<>'42' AND SUBSTR(COD_CONTRATTO,1,2)<>'43' AND " _
                            & "SUBSTR(COD_CONTRATTO,1,6)<>'000000' AND " _
                            & " (anagrafica.ragione_sociale IS NULL OR anagrafica.ragione_sociale='') AND  ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                            & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'   AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=unita_contrattuale.id_unita AND " _
                            & "unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL  AND  COD_CONTRATTO IS NOT NULL "


        sStringaSql1 = sStringaSql1 & " ORDER BY descrizione asc,indirizzi.civico asc,anagrafica.cognome ASC,anagrafica.nome asc"


        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql1, par.OracleConn)
        Dim ds As New Data.DataSet()
        Dim dt As New System.Data.DataTable

        'da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")
        da.Fill(dt)
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()
        Session.Add("MIADT", dt)


        Label9.Text = "  - Totale:" & dt.Rows.Count



        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        SalvaGruppo()
    End Sub

    Private Function SalvaGruppo()
        Try

            Dim Registro As Long = -1
            Dim IDAU As Long
            Dim row As System.Data.DataRow
            Dim dt As New System.Data.DataTable

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim I As Long = 0

            par.cmd.CommandText = "select * from utenza_gruppi_lavoro where ID_BANDO_AU=" & IDAU & " and nome_gruppo='" & par.PulisciStrSql(txtNomeGruppo.Text.ToUpper) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                txtNomeGruppo.Text = txtNomeGruppo.Text & "(1)"
            End If
            myReader.Close()

            Dim IdGruppo As Long = -1
            par.cmd.CommandText = "INSERT INTO UTENZA_GRUPPI_LAVORO (ID,NOME_GRUPPO,DATA_CREAZIONE,ID_OPERATORE,ID_BANDO_AU,FL_ABUSIVI) VALUES (SEQ_UTENZA_GRUPPI.NEXTVAL,'" & par.PulisciStrSql(UCase(txtNomeGruppo.Text)) & "','" & Format(Now, "yyyyMMdd") & "'," & Session.Item("ID_OPERATORE") & "," & IndiceAU & ",1)"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "SELECT SEQ_UTENZA_GRUPPI.CURRVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                IdGruppo = myReader(0)
            End If
            myReader.Close()

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            For Each row In dt.Rows

                par.cmd.CommandText = "INSERT INTO UTENZA_GRUPPI_DICHIARAZIONI (ID,ID_GRUPPO,ID_DICHIARAZIONE,APPLICA_AU,ID_CONTRATTO) VALUES (SEQ_UTENZA_GR_DIC.NEXTVAL," & IdGruppo & ",NULL,0," & par.IfNull(dt.Rows(I).Item("IDC"), "") & ")"
                par.cmd.ExecuteNonQuery()

                I = I + 1

            Next

            'par.myTrans.Rollback()
            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            txtNomeGruppo.Text = ""
            Response.Write("<script>alert('Operazione Effettuata! Le dichiarazioni selezionate sono state inserite nel nuovo gruppo. La maschera sarà ricaricata');</script>")

            Dim Str As String
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"
            Response.Write(Str)
            Response.Flush()
            BindGrid()
        Catch ex As Exception
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function

    Public Property IndiceAU() As String
        Get
            If Not (ViewState("par_IndiceAU") Is Nothing) Then
                Return CStr(ViewState("par_IndiceAU"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceAU") = value
        End Set
    End Property
End Class
