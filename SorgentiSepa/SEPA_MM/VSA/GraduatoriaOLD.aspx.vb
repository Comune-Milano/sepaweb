Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class VSA_Graduatoria
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
        Response.Flush()

        If Not IsPostBack Then
            Cerca()
        End If
    End Sub

    Private Function Cerca()
        'sStringaSQL1 = "SELECT  (DOMANDE_BANDO_VSA_MOT_CAMBI.AI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||dOMANDE_BANDO_VSA_MOT_CAMBI.RU||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.RI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AA||DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.VC||DOMANDE_BANDO_VSA_MOT_CAMBI.HM||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HA||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='1'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HT||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='0'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HP||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66 AND PERC_INVAL<100),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AN||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND (SYSDATE-TO_DATE(DATA_NASCITA,'YYYYmmdd'))/365>65),'00000000'))||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.FS||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.PV||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA AS ORDINE," _
        '& "DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.PG, COMP_NUCLEO_VSA.COGNOME, COMP_NUCLEO_VSA.NOME, DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.PV,0,'NO',1,'SI') AS PV,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.FS,0,'NO',1,'SI') AS FS,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AN,0,'NO',1,'SI') AS AN,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HP,0,'NO',1,'SI') AS HP,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HT,0,'NO',1,'SI') AS HT,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HA,0,'NO',1,'SI') AS HA,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HM,0,'NO',1,'SI') AS HM,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AA,0,'NO',1,'SI') AS AA,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AI,0,'NO',1,'SI') AS AI, DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RU,0,'NO',1,'SI') AS RU,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RI,0,'NO',1,'SI') AS RI FROM COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,DOMANDE_BANDO_VSA_MOT_CAMBI WHERE COMP_NUCLEO_VSA.PROGR=0 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA_MOT_CAMBI.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND (DOMANDE_BANDO_VSA.ID_STATO='8' OR DOMANDE_BANDO_VSA.ID_STATO='9') AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=4  ORDER BY ORDINE DESC"


        '        sStringaSQL1 = "SELECT  " _
        '& "(DOMANDE_BANDO_VSA_MOT_CAMBI.AI||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AI=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.RU||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.RU=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.RI||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.RI=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.AA||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000') END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V16||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V5||DOMANDE_BANDO_VSA_MOT_CAMBI.IV||DOMANDE_BANDO_VSA_MOT_CAMBI.HM||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V17||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000') END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V16||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.HA||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V17||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=100 AND INDENNITA_ACC='1'),'00000000') END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V16||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.HT||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V17||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=100 AND INDENNITA_ACC='0'),'00000000') END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V16||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.HP||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V17||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66 AND PERC_INVAL<=99),'00000000') END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V16||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.AN||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V17||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND (SYSDATE-TO_DATE(DATA_NASCITA,'YYYYmmdd'))/365>65),'00000000') END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.V16||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.FS||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.PV||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.PV=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.CD||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.CD=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
        '& "DOMANDE_BANDO_VSA_MOT_CAMBI.AE||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AE=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END) AS ORDINE," _
        '& "DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.PG, COMP_NUCLEO_VSA.COGNOME, COMP_NUCLEO_VSA.NOME, TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_RICHIESTA," _
        '& "TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AI,0,'NO',1,'SI') AS AI,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RU,0,'NO',1,'SI') AS RU," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RI,0,'NO',1,'SI') AS RI," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AA,0,'NO',1,'SI') AS AA,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.IV,0,'NO',1,'SI') AS IV,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HM,0,'NO',1,'SI') AS HM," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HA,0,'NO',1,'SI') AS HA,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HT,0,'NO',1,'SI') AS HT,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HP,0,'NO',1,'SI') AS HP," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AN,0,'NO',1,'SI') AS AN,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.FS,0,'NO',1,'SI') AS FS,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.PV,0,'NO',1,'SI') AS PV," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.CD,0,'NO',1,'SI') AS CD,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AE,0,'NO',1,'SI') AS AE FROM SISCOM_MI.TIPO_LIVELLO_PIANO, DOMANDE_VSA_ALLOGGIO, " _
        '& "COMP_NUCLEO_VSA, DOMANDE_BANDO_VSA, DOMANDE_BANDO_VSA_MOT_CAMBI  WHERE TIPO_LIVELLO_PIANO.COD=DOMANDE_VSA_ALLOGGIO.PIANO AND DOMANDE_VSA_ALLOGGIO.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND " _
        '& "COMP_NUCLEO_VSA.PROGR=0 AND  COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA_MOT_CAMBI.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND  " _
        '& "(DOMANDE_BANDO_VSA.ID_STATO='8' OR DOMANDE_BANDO_VSA.ID_STATO='9') AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=4  ORDER BY ORDINE DESC"
        sStringaSQL1 = "SELECT  " _
& "(CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AI=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AI=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.RU=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.RU=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.RI=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.RI=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AA=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.IV=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.IV=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000') END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V5=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.HM=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000') END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.HA=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=100 AND INDENNITA_ACC='1'),'00000000') END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.HT=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=100 AND INDENNITA_ACC='0'),'00000000') END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.HP=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66 AND PERC_INVAL<=99),'00000000') END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AN=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V17=0 THEN '00000000' ELSE NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND (SYSDATE-TO_DATE(DATA_NASCITA,'YYYYmmdd'))/365>65),'00000000') END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.FS=0 THEN '1' ELSE '0' END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.V16=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.PV=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.PV=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.CD=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.CD=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END||" _
& "CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AE=0 THEN '1' ELSE '0' END||CASE WHEN DOMANDE_BANDO_VSA_MOT_CAMBI.AE=0 THEN '00000000' ELSE DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA END) AS ORDINE," _
& "DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.PG, COMP_NUCLEO_VSA.COGNOME, " _
& "COMP_NUCLEO_VSA.NOME, TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_RICHIESTA," _
& "TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AI,0,'NO',1,'SI') AS AI,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RU,0,'NO',1,'SI') AS RU," _
& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RI,0,'NO',1,'SI') AS RI,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AA,0,'NO',1,'SI') AS AA," _
& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.IV,0,'NO',1,'SI') AS IV,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HM,0,'NO',1,'SI') AS HM," _
& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HA,0,'NO',1,'SI') AS HA,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HT,0,'NO',1,'SI') AS HT," _
& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HP,0,'NO',1,'SI') AS HP,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AN,0,'NO',1,'SI') AS AN," _
& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.FS,0,'NO',1,'SI') AS FS,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.PV,0,'NO',1,'SI') AS PV," _
& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.CD,0,'NO',1,'SI') AS CD,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AE,0,'NO',1,'SI') AS AE FROM SISCOM_MI.TIPO_LIVELLO_PIANO, " _
& "DOMANDE_VSA_ALLOGGIO, COMP_NUCLEO_VSA, DOMANDE_BANDO_VSA, DOMANDE_BANDO_VSA_MOT_CAMBI  " _
& "WHERE TIPO_LIVELLO_PIANO.COD=DOMANDE_VSA_ALLOGGIO.PIANO AND DOMANDE_VSA_ALLOGGIO.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND " _
& "COMP_NUCLEO_VSA.PROGR=0 AND  COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA_MOT_CAMBI.ID_DOMANDA=DOMANDE_BANDO_VSA.ID " _
& "AND  (DOMANDE_BANDO_VSA.ID_STATO='8' OR DOMANDE_BANDO_VSA.ID_STATO='9') AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=4  ORDER BY ORDINE ASC"

        BindGrid()



    End Function

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

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa")


        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label7.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TextBox7').value='Hai selezionato il PG: " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        If LBLID.Value <> "" And LBLID.Value <> "-1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "update domande_bando_vsa set id_stato='9' where id=" & LBLID.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione Effettuata. Pronto per essere invitato!');</script>")
                BindGrid()

            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label8.Visible = True
                Label8.Text = ex.Message
            End Try
        Else
            Response.Write("<script>alert('Nessun nominativo selezionato!');</script>")
        End If
    End Sub



    Private Sub ExportXLS()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim dt As New Data.DataTable
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try
            par.OracleConn.Open()
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                i = 0
                With myExcelFile

                    .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
                    .PrintGridLines = False
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                    .SetDefaultRowHeight(14)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                    .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)





                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "PG", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA R.", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COGNOME", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "NOME", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PIANO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "AI", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "RU", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "RI", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "AA", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "IV", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "HM", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "HA", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "HT", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "HP", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "AN", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "FS", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "PV", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "CD", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "AE", 0)


                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PG"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.FormattaData(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_RICHIESTA"), ""))))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COGNOME"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AI"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RU"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IV"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("HM"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("HA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("HT"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("HP"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AN"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FS"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PV"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CD"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AE"), "")))


                        i = i + 1
                        K = K + 1
                    Next

                    .CloseFile()
                End With

            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)
            Response.Write("<script>window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');</script>")



            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try



    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        ExportXLS()
    End Sub
End Class
