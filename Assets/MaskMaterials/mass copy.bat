
PUSHD
FOR /R %%a IN (*.dds) DO (
     	copy "%%a" "C:\Users\Nick\Documents\Downloads\payday2\units\payday2\matcaps\mask_Materials\%%~nxa"
)

FOR /R %%a IN (*.model) DO (
	copy "%%a" "C:\Users\Nick\Documents\Downloads\payday2\units\payday2\matcaps\mask_Materials\%%~nxa"
)
PAUSE