FROM mono:6.12

RUN sed -i 's|deb.debian.org/debian-security|archive.debian.org/debian-security|g' /etc/apt/sources.list \
    && sed -i 's|deb.debian.org/debian|archive.debian.org/debian|g' /etc/apt/sources.list \
    && sed -i '/buster-updates/d' /etc/apt/sources.list \
    && apt-get -o Acquire::Check-Valid-Until=false -o Acquire::AllowInsecureRepositories=true update \
    && apt-get install -y --no-install-recommends nuget mono-xsp4 \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY . .

RUN sed -i 's#connectionString=\"[^\"]*\"#connectionString=\"Server=sql,1433;Database=CorporateWebDB;User ID=sa;Password=Strong!Passw0rd;Encrypt=False\"#' /app/KurumsalWeb/Web.config \
    && sed -i 's#connectionString=\"Server=localhost\\\\sqlexpress;Database=CorporateWebDB;Integrated Security=True\"#connectionString=\"Server=sql,1433;Database=CorporateWebDB;User ID=sa;Password=Strong!Passw0rd;Encrypt=False\"#' /app/KurumsalWeb/Web.config \
    && sed -i 's#connectionString=\"Server=localhost\\sqlexpress;Database=CorporateWebDB;Integrated Security=True\"#connectionString=\"Server=sql,1433;Database=CorporateWebDB;User ID=sa;Password=Strong!Passw0rd;Encrypt=False\"#' /app/KurumsalWeb/Web.config
RUN sed -i 's#__SET_ADMIN_EMAIL__#admin@local.dev#' /app/KurumsalWeb/Web.config \
    && sed -i 's#__SET_ADMIN_PASSWORD__#Admin123!#' /app/KurumsalWeb/Web.config
RUN sed -i 's#<httpRuntime targetFramework="4.7.2" />#<httpRuntime targetFramework="4.7.2" />\\n    <customErrors mode="Off" />#' /app/KurumsalWeb/Web.config

RUN nuget restore KurumsalWeb.sln

WORKDIR /app/KurumsalWeb
EXPOSE 8080

CMD ["xsp4", "--port", "8080", "--address", "0.0.0.0", "--nonstop", "--root", "/app/KurumsalWeb"]
