@ECHO OFF
PUSHD .
FOR /R %%d IN (.) DO (
    cd "%%d"
    IF EXIST *.texture (
       REN *.texture *.dds
    )
)
POPD