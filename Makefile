.DEFAULT_GOAL := all
build:
	dotnet publish -c PublishRelease -r $(target)
	mv ./bin/PublishRelease/net7.0/$(target)/publish ./release/$(target)

clean:
	rm -rf bin/PublishRelease/net7.0/
	rm -rf release
	mkdir release

archive:
	git archive HEAD -o 22810214.zip --format=zip \
		--prefix=/release/linux/ --add-file=release/linux-x64/NMLT \
		--prefix=/release/osx/ --add-file=release/osx-arm64/NMLT \
		--prefix=/release/win/ --add-file=release/win-x64/NMLT.exe \
		--prefix=/ --add-file=22810214.docx

all:
	make clean
	make build target=osx-arm64
	make build target=linux-x64
	make build target=win-x64
